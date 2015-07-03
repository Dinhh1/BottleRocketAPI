using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BottleRocket.Models
{
    [Table("PickupReceipts")]
    public class PickupReceipt
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ScheduledPickupId { get; set; }
        public Decimal? AluminumTotal { get; set; }
        public Decimal? GlassTotal { get; set; }
        public Decimal? Plastic1Total { get; set; }
        public Decimal? Plastic2Total { get; set; }
        public Decimal? OverallTotal { get; set; }
        DateTime DateCreated { get; set; }
        DateTime LastUpdated { get; set; }
    }

    public class PickupReceiptDbContext : DbContext
    {
        public DbSet<PickupReceipt> PickupReceipts { get; set; }
        public PickupReceiptDbContext()
            : base("DefaultConnection")
        {
        }

        public static PickupReceiptDbContext Create()
        {
            return new PickupReceiptDbContext();
        }
    }
}