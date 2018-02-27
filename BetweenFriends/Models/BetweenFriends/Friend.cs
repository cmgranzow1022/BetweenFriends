using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Friend
    {
        [Key]
        public int FriendId { get; set;}
        [ForeignKey("Customer")]
        public int RequesterId { get; set; }
        public Customer Requester { get; set; }

        [ForeignKey("Customer")]
        public int RequesteeId { get; set; }
        public Customer Requestee { get; set; }
        public bool Approve { get; set; }
        public bool Deny { get; set; }
    }
}

