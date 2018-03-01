using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BetweenFriends.Models;
using BetweenFriends.Models.BetweenFriendsModels;

namespace BetweenFriends.Controllers
{
    public class PendingRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PendingRequests
        public ActionResult Index()
        {
            var pendingRequests = db.PendingRequests.Include(p => p.RequesteeId).Include(p => p.RequesterId);
            return View(pendingRequests.ToList());
        }

        // GET: PendingRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendingRequests pendingRequests = db.PendingRequests.Find(id);
            if (pendingRequests == null)
            {
                return HttpNotFound();
            }
            return View(pendingRequests);
        }

        // GET: PendingRequests/Create
        public ActionResult Create()
        {
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: PendingRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PendingRequestId,CustomerIdOne,CustomerIdTwo")] PendingRequests pendingRequests)
        {
            if (ModelState.IsValid)
            {
                db.PendingRequests.Add(pendingRequests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdOne);
            return View(pendingRequests);
        }

        // GET: PendingRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendingRequests pendingRequests = db.PendingRequests.Find(id);
            if (pendingRequests == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdOne);
            return View(pendingRequests);
        }

        // POST: PendingRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PendingRequestId,CustomerIdOne,CustomerIdTwo")] PendingRequests pendingRequests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pendingRequests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerIdTwo = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdTwo);
            ViewBag.CustomerIdOne = new SelectList(db.Customers, "CustomerId", "FirstName", pendingRequests.CustomerIdOne);
            return View(pendingRequests);
        }

        // GET: PendingRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendingRequests pendingRequests = db.PendingRequests.Find(id);
            if (pendingRequests == null)
            {
                return HttpNotFound();
            }
            return View(pendingRequests);
        }

        // POST: PendingRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PendingRequests pendingRequests = db.PendingRequests.Find(id);
            db.PendingRequests.Remove(pendingRequests);
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
