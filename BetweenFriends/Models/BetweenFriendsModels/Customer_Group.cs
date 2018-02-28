using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Customer_Group 
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
    }
}