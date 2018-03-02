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
        public List<PendingRequests> Requests { get; set; }
        public List<Friend> ConfirmedFriends { get; set; }
        public List<SelectListItem> AvailableToRequest { get; set; }
        [Display(Name = "Available Users")]
        public string RequestedCustomerId { get; set; }
        public FriendsViewModel()
        {
            Requests = new List<PendingRequests>();
            ConfirmedFriends = new List<Friend>();
            AvailableToRequest = new List<SelectListItem>();
        }
    }
}