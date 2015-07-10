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
        public Decimal TotalAmount { get; set; }
        public int PickupCycleId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}