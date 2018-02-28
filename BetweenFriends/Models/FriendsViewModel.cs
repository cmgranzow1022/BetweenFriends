using BetweenFriends.Models.BetweenFriends;
using BetweenFriends.Models.BetweenFriendsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models
{
    public class FriendsViewModel
    {
        public Customer customer { get; set; }
        public List<Customer> AllCustomers { get; set; }
        public List<PendingRequests> Requests { get; set; }
        public List<Customer> AlreadyFriends { get; set; }
        public List<Friend> ConfirmedFriends { get; set; }
        public List<Friend> FriendPairs { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Customer> AvailableToRequest { get; set; }
        public FriendsViewModel()
        {
            customer = new Customer();
            AllCustomers = new List<Customer>();
            Requests = new List<PendingRequests>();
            AlreadyFriends = new List<Customer>();
            ConfirmedFriends = new List<Friend>();
            FriendPairs = new List<Friend>();
            Users = new List<ApplicationUser>();
            AvailableToRequest = new List<Customer>();
        }


    }
}