using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BottleRocket.Models
{
    public class ScheduledPickup
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DatePickedUp { get; set; }
        public bool IsPickedUp { get; set; }
        public int AddressId { get; set; }

    }

    public class ScheduledPickupDbContext : DbContext
    {
        public DbSet<ScheduledPickup> ScheduledPickups { get; set; }
        public ScheduledPickupDbContext()
            : base("DefaultConnection")
        {
        }

        public static ScheduledPickupDbContext Create()
        {
            return new ScheduledPickupDbContext();
        }
    }
}