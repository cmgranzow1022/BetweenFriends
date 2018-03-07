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
using GeoCoordinatePortable;


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

        public ActionResult DisplayCenterPoint()
        {
            List<GeoCoordinate> listCoordinate = new List<GeoCoordinate>();
            listCoordinate.Add(new GeoCoordinate() { Latitude = 0, Longitude = 0 });
            GeoCoordinate centerCoordinate = GetCentralGeoCoordinate(listCoordinate);
            Console.WriteLine("Lat:" + centerCoordinate.Latitude + ",Lon:" + centerCoordinate.Longitude);
            return View();
        }
        public static GeoCoordinate GetCentralGeoCoordinate(List<GeoCoordinate> geoCoordinates)
        {
            if (geoCoordinates.Count == 1)
            {
                return geoCoordinates.Single();
            }
            double x = 0, y = 0, z = 0;
            foreach (var geoCoordinate in geoCoordinates)
            {
                var latitude = geoCoordinate.Latitude * Math.PI / 180;
                var longitude = geoCoordinate.Longitude * Math.PI / 180;

                x += Math.Cos(latitude) * Math.Cos(longitude);
                y += Math.Cos(latitude) * Math.Sin(longitude);
                z += Math.Sin(latitude);
            }
            var total = geoCoordinates.Count;
            x = x / total;
            y = y / total;
            z = z / total;
            var centralLongitude = Math.Atan2(y, x);
            var centralSquareRoot = Math.Sqrt(x * x + y * y);
            var centralLatitude = Math.Atan2(z, centralSquareRoot);
            return new GeoCoordinate(centralLatitude * 180 / Math.PI, centralLongitude * 180 / Math.PI);
        }


        public ActionResult CreateGroup()
        {
            return View("Group");
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
            List<Group> groups = db.Groups.ToList();
            model.currentGroup = groups[groups.Count - 1].GroupId;
            Customer_Group customerGroup = new Customer_Group();
            customerGroup.GroupId = model.currentGroup;
            List<Customer> AllCustomers = db.Customers.ToList();
            var loggedInUserId = User.Identity.GetUserId();
            customerGroup.CustomerId = (from x in AllCustomers where x.UserId == loggedInUserId select x.CustomerId).FirstOrDefault();
            db.Customer_Group.Add(customerGroup);
            db.SaveChanges();
            return RedirectToAction("FriendsToGroup", "Groups");
        }

        public ActionResult FriendsToGroup()
        {
            List<Customer> AllCustomers = db.Customers.ToList();
            GroupViewModel Groups = new GroupViewModel();
            List<Friend> Friends = db.Friends.ToList();
            string currentUserId = User.Identity.GetUserId();
            Groups.LoggedInCustomer = (from x in AllCustomers where x.UserId == currentUserId select x).FirstOrDefault();
            List<int?> ConfirmedFriends = (from f in Friends where f.CustomerIdOne == Groups.LoggedInCustomer.CustomerId select f.CustomerIdTwo).ToList();
            List<int?> ConfirmedFriendsTwo = (from f in Friends where f.CustomerIdTwo == Groups.LoggedInCustomer.CustomerId select f.CustomerIdOne).ToList();
            ConfirmedFriends.AddRange(ConfirmedFriendsTwo);
            List<Group> group = new List<Group>();
            group = db.Groups.ToList();
            Groups.currentGroup = group[group.Count - 1].GroupId;
            Groups.customerGroup = db.Customer_Group.ToList();
            for (int i = 0; i < AllCustomers.Count; i++)
            {
                for (int j = 0; j < ConfirmedFriends.Count; j++)
                {
                    if (AllCustomers[i].CustomerId == ConfirmedFriends[j])
                    {
                        Groups.FriendsInList.Add(AllCustomers[i]);
                    }
                }
            }
            for (int i = 0; i < Groups.FriendsInList.Count; i++)
            {

                    SelectListItem item = new SelectListItem
                    {
                        Text = Groups.FriendsInList[i].FullName,
                        Value = Groups.FriendsInList[i].CustomerId.ToString()
                    };
                    Groups.AvailableToAdd.Add(item);
            }
            
            return View("FriendsToGroup",Groups);
        }

        [HttpPost]
        public ActionResult FriendsToGroup(GroupViewModel model)
        {
            List<Group> group = new List<Group>();
            group = db.Groups.ToList();
            List<Customer> AllCustomers = new List<Customer>();
            List<Customer> CustomersInGroup = new List<Customer>();
            List<int> CustomerIdsInGroup = new List<int>();
            AllCustomers = db.Customers.ToList();
            List<Customer_Group> CustomerGroup = new List<Customer_Group>();
            CustomerGroup = db.Customer_Group.ToList();
            Customer_Group newCustomerGroup = new Customer_Group();
            model.currentGroup = group[group.Count - 1].GroupId;
   
            newCustomerGroup.GroupId = model.currentGroup;
            newCustomerGroup.CustomerId = Int32.Parse(model.RequestedCustomerId);

            model.CustomersInGroup = CustomersInGroup;
            db.Customer_Group.Add(newCustomerGroup);
           
            db.SaveChanges();
            return RedirectToAction("FriendsToGroup","Groups");
        }

        public ActionResult MapView(GroupViewModel model)
        {
            List<Group> group = new List<Group>();
            group = db.Groups.ToList();
            List<Customer_Address> customerAddresses = new List<Customer_Address>();
            customerAddresses = db.Customer_Addresses.ToList();
            List<Address> mapAddresses = new List<Address>();
            model.currentGroup = group[group.Count - 1].GroupId;

            List<Customer> customersInGroup = (from y in db.Customer_Group where y.GroupId == model.currentGroup select y.Customer).ToList();
            for (int i = 0; i < customersInGroup.Count; i ++)
            {
                for(int j = 0; j < customerAddresses.Count; j++)
                {
                   if(customerAddresses[j].CustomerId == customersInGroup[i].CustomerId)
                    {
                        mapAddresses.Add(customerAddresses[j].Address);
                    }
                }
                
            }

            return View(mapAddresses);

        }

        


        public ActionResult RestaurantView()
        {
            return View("RestaurantView");
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


