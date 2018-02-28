using BetweenFriends.Models.BetweenFriends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriendsModels
{
    public class RestaurantSelection
    { 
        [Key]
        public int RestaurantSelectionId { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public string Date { get; set; }
    }
}