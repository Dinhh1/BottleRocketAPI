﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
namespace BottleRocket.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<ScheduledPickup> ScheduledPickups { get; set; }
        public DbSet<Global> Globals { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<PickupCycle> PickupCycles { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}