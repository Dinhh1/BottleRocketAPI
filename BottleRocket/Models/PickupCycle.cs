using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BottleRocket.Models
{
    [Table("PickupCycles")]
    public class PickupCycle
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Decimal AluminumWeight { get; set; }
        public Decimal GlassWeight { get; set; }
        public Decimal StandardPlasticWeight { get; set; }
        public Decimal MiscPlasticWeight { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public Decimal TotalBags { get; set; }
        public string Notes { get; set; }
        public int CommunityId { get; set; }

    }
}