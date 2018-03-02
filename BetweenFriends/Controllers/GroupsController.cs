using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BetweenFriends.Models;
using BetweenFriends.Models.BetweenFriends;
using Microsoft.AspNet.Identity;


namespace BetweenFriends.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            var groups = db.Groups;
            return View(groups.ToList());
        }



        public ActionResult CreateGroup()
        {
            List<Customer> AllCustomers = db.Customers.ToList();
            GroupViewModel Groups = new GroupViewModel();
            List<Friend> Friends = db.Friends.ToList();
            List<Customer> FriendsInList = new List<Customer>();
            string currentUserId = User.Identity.GetUserId();
            Groups.LoggedInCustomer = (from x in AllCustomers where x.UserId == currentUserId select x).FirstOrDefault();
            List<int?> ConfirmedFriends = (from f in Friends where f.CustomerIdOne == Groups.LoggedInCustomer.CustomerId select f.CustomerIdTwo).ToList();
            List<int?> ConfirmedFriendsTwo = (from f in Friends where f.CustomerIdTwo == Groups.LoggedInCustomer.CustomerId select f.CustomerIdOne).ToList();
            ConfirmedFriends.AddRange(ConfirmedFriendsTwo);
            for (int i = 0; i < AllCustomers.Count; i++)
            {
                for (int j = 0; j < ConfirmedFriends.Count; j++)
                {
                    if (AllCustomers[i].CustomerId == ConfirmedFriends[j])
                    {
                        FriendsInList.Add(AllCustomers[i]);
                    }
                }
            }
            for (int i = 0; i < FriendsInList.Count; i++)
            {

                    SelectListItem item = new SelectListItem
                    {
                        Text = FriendsInList[i].FullName,
                        Value = FriendsInList[i].CustomerId.ToString()
                    };
                    Groups.AvailableToAdd.Add(item);
            }
            return View("Group",Groups);
        }

        [HttpPost]
        public ActionResult CreateGroup([Bind(Include = "Date,Time,GroupId,GroupName")]GroupViewModel model)
        {
            Group group = new Group();
            string userId = User.Identity.GetUserId();
            group.Date = model.Date;
            group.Time = model.Time;
            group.GroupName = model.GroupName;
            db.Groups.Add(group);
            db.SaveChanges();
            return RedirectToAction("CreateGroup", "Groups");
        }



        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,Date,Time,Attending")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,Date,Time,GroupName")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


