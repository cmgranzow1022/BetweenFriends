using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class CustomerAddress
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer customer { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address address { get; set; }
    }
}