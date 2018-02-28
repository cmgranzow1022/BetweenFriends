using BetweenFriends.Models.BetweenFriends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriendsModels
{
    public class PendingRequests
    {
        [Key]
        public int PendingRequestId { get; set; }

        [ForeignKey("RequesterId"), Column(Order = 0)]
        public int? CustomerIdOne { get; set; }
        public virtual Customer RequesterId { get; set; }

        [ForeignKey("RequesteeId"), Column(Order = 1)]
        public int? CustomerIdTwo { get; set; }
        public virtual Customer RequesteeId { get; set; }
    }
}