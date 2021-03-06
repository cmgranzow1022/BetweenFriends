﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BetweenFriends.Models.BetweenFriends;
using BetweenFriends.Models.BetweenFriendsModels;

namespace BetweenFriends.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BetweenFriends.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<BetweenFriends.Customer> Customers { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Veto> Vetoes { get; set; }
        public DbSet<RestaurantSelection> RestaurantSelection { get; set; }

        public DbSet<PendingRequests> PendingRequests { get; set; }
        public DbSet<Customer_Address> Customer_Addresses { get; set; }
        public DbSet<Customer_Group> Customer_Group { get; set; }


    }

}