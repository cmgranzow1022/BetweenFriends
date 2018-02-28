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
using System.Threading.Tasks;

namespace BetweenFriends.Controllers
{
    public class FriendsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Customer> customersList = new List<Customer>();

        public List<Customer> FriendRequest()
        {
            customersList = db.Customers.ToList();
            return customersList;
        }
        public ActionResult RequestFriend()
        {
            FriendsViewModel Friends = new FriendsViewModel();
            Friends.AllCustomers = db.Customers.ToList();
            Friends.FriendPairs = db.Friends.ToList();
 
            LoginViewModel model = new LoginViewModel();
          
            string currentUserId = User.Identity.GetUserId();
            Friends.customer = (from x in Friends.AllCustomers where x.UserId == currentUserId select x).FirstOrDefault();
            List<int?> ConfirmedFriends = (from f in Friends.FriendPairs where f.CustomerIdOne == Friends.customer.CustomerId select f.CustomerIdTwo).ToList();
            List<int?> ConfirmedFriendsTwo = (from f in Friends.FriendPairs where f.CustomerIdTwo == Friends.customer.CustomerId select f.CustomerIdOne).ToList();
            ConfirmedFriends.AddRange(ConfirmedFriendsTwo);
            for (int i = 0; i < Friends.AllCustomers.Count; i++)
            {
                for (int j = 0; j < Friends.ConfirmedFriends.Count; j++)
                {
                    if(Friends.AllCustomers[i].CustomerId == ConfirmedFriends[j])
                    {
                        Friends.AlreadyFriends.Add(Friends.AllCustomers[i]);
                    }
                }
            }

            for(int i=0; i < Friends.AllCustomers.Count; i++)
            {
                if (Friends.AlreadyFriends[i] != Friends.AllCustomers[i])
                {
                    Friends.AvailableToRequest.Add(Friends.AllCustomers[i]);
                }
          
            }

            return View(Friends.AvailableToRequest);
        }

        
        public ActionResult PotentialFriends()
        {
            return View();
        }
       
    
        


        // GET: Friends
        public ActionResult Index()
        {
            var friends = db.Friends.Include(f => f.RequesteeId).Include(f => f.RequesterId);
            return View(friends.ToList());
        }

        // GET: Friends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // GET: Friends/Create
        public ActionResult Create()
        {
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FriendId,CustomerIdOne,CustomerIdTwo")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Friends.Add(friend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdOne);
            return View(friend);
        }

        // GET: Friends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdOne);
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FriendId,CustomerIdOne,CustomerIdTwo")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", friend.CustomerIdOne);
            return View(friend);
        }

        // GET: Friends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Friend friend = db.Friends.Find(id);
            db.Friends.Remove(friend);
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
