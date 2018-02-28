using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Group 
    {
        [Key]
        public int GroupId { get; set; }

        [ForeignKey("CustomerOwner")]
        public int CustomerOwnerId { get; set; }
        public virtual Customer CustomerOwner { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
 
        public string Date { get; set; } 
        public string Time { get; set; }
        public bool Attending { get; set; }
    }
}