using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BetweenFriends.Models.BetweenFriends
{
    public class Customer 
    {
        [Key]
        public int CustomerId { get; set; }
        [Display (Name = "First Name")]
        public string FirstName { get; set; }
        [Display (Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display (Name = "Cell Phone Number")]
        public string CellPhoneNumber { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual ApplicationUser User { get; set; }
    }
    }
