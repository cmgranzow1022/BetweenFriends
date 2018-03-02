using BetweenFriends.Models.BetweenFriends;
using BetweenFriends.Models.BetweenFriendsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetweenFriends.Models
{
    public class FriendsViewModel
    {
        public Customer LoggedInCustomer { get; set; }
        //public List<Customer> AllCustomers { get; set; }
        public List<PendingRequests> Requests { get; set; }
        //public List<Customer> AlreadyFriends { get; set; }
        public List<Friend> ConfirmedFriends { get; set; }
        //public List<Friend> FriendPairs { get; set; }
        //public List<ApplicationUser> Users { get; set; }
        public List<SelectListItem> AvailableToRequest { get; set; }
        [Display(Name = "Available Users")]
        public string RequestedCustomerId { get; set; }
        //public List<PendingRequests> PRequest { get; set; }
        public FriendsViewModel()
        {
            //LoggedInCustomer = new Customer();
            //AllCustomers = new List<Customer>();
            Requests = new List<PendingRequests>();
            //AlreadyFriends = new List<Customer>();
            ConfirmedFriends = new List<Friend>();
            //FriendPairs = new List<Friend>();
            //Users = new List<ApplicationUser>();
            AvailableToRequest = new List<SelectListItem>();
        }




    }
}