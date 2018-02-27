using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group group { get; set; }
        public string Date { get; set; }
        public string Cuisine { get; set; }
    }
}