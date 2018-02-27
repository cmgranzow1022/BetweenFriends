using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerOwner { get; set; }
        public Customer customer { get; set; }


    }
}