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
using BetweenFriends.Models.BetweenFriendsModels;

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

        public ActionResult PendingFriend()
        {
            FriendsViewModel Friends = new FriendsViewModel();
            List<Customer> AllCustomers = db.Customers.ToList();
            List<PendingRequests> PRequest = db.PendingRequests.ToList();
            string currentUserId = User.Identity.GetUserId();
            Customer LoggedInCustomer = (from x in AllCustomers where x.UserId == currentUserId select x).FirstOrDefault();
            List<PendingRequests> PendingList = (from x in db.PendingRequests.Include("RequesterId").Include("RequesteeId") where x.CustomerIdTwo == LoggedInCustomer.CustomerId select x).ToList();
            //List<Customer> PendingCustomerList = new List<Customer>();
            //for(int i = 0; i < PendingList.Count; i++)
            //{
            //    for (int j = 0; j < AllCustomers.Count; j++)
            //    {
            //        if (PendingList[i] == AllCustomers[j].CustomerId)
            //        {
            //            PendingCustomerList.Add(AllCustomers[j]);
            //        }
            //    }
            //}
            return View();
        }

        public ActionResult RequestFriend()
        {

            FriendsViewModel Friends = new FriendsViewModel();
            List<Customer> AlreadyFriends = new List<Customer>();
            List<Customer>AllCustomers = db.Customers.ToList();
            List<Friend>FriendPairs = db.Friends.ToList();
            string currentUserId = User.Identity.GetUserId();
            Friends.LoggedInCustomer = (from x in AllCustomers where x.UserId == currentUserId select x).FirstOrDefault();
            Friends.Requests = (from x in db.PendingRequests.Include("RequesterId").Include("RequesteeId") where x.CustomerIdTwo == Friends.LoggedInCustomer.CustomerId || x.CustomerIdOne == Friends.LoggedInCustomer.CustomerId select x).ToList();
            AllCustomers.Remove(Friends.LoggedInCustomer);
            List<int?> ConfirmedFriends = (from f in FriendPairs where f.CustomerIdOne == Friends.LoggedInCustomer.CustomerId select f.CustomerIdTwo).ToList();
            List<int?> ConfirmedFriendsTwo = (from f in FriendPairs where f.CustomerIdTwo == Friends.LoggedInCustomer.CustomerId select f.CustomerIdOne).ToList();
            ConfirmedFriends.AddRange(ConfirmedFriendsTwo);
            for (int i = 0; i < AllCustomers.Count; i++)
            {
                for (int j = 0; j < Friends.ConfirmedFriends.Count; j++)
                {
                    if(AllCustomers[i].CustomerId == ConfirmedFriends[j])
                    {
                        AlreadyFriends.Add(AllCustomers[i]);
                    }
                }
            }

            for(int i=0; i < AllCustomers.Count; i++)
            {
                bool matchFound = false;
                for (int j = 0;  j< AlreadyFriends.Count; j++)
                {
                    if (AlreadyFriends[j] == AllCustomers[i])
                    {
                        matchFound = true;
                    }
                }
                if (!matchFound)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Text = AllCustomers[i].FullName,
                        Value = AllCustomers[i].CustomerId.ToString()
                    };
                    Friends.AvailableToRequest.Add(item);
                }
            }
            return View("Friends",Friends);
        }

        [HttpPost]
        public ActionResult RequestFriend(FriendsViewModel model)
        {
            int customerIdTwo = Int32.Parse(model.RequestedCustomerId);
            string userId = User.Identity.GetUserId();
            int customerIdOne = (from x in db.Customers where x.UserId == userId select x.CustomerId).FirstOrDefault();
            PendingRequests request = new PendingRequests();
            request.CustomerIdOne = customerIdOne;
            request.CustomerIdTwo = customerIdTwo;
            db.PendingRequests.Add(request);
            db.SaveChanges();
  
            return RedirectToAction("Index","Home");
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
