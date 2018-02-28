using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Address
    { 
        [Key]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(5)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}