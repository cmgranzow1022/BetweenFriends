﻿using BetweenFriends.Models.BetweenFriends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetweenFriends.Models
{
    public class GroupViewModel
    {
        public Customer LoggedInCustomer { get; set; }
        public List<Friend> ListOfFriends { get; set; }
        public List<SelectListItem> AvailableToAdd { get; set; }
        public string RequestedCustomerId { get; set; }
        public List<Customer_Group> customerGroup { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }



        public GroupViewModel()
        {
            ListOfFriends = new List<Friend>();
            AvailableToAdd = new List<SelectListItem>();
            customerGroup = new List<Customer_Group>();
        }


    }
}