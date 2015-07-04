using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BottleRocket.Models
{
    public class PickupCycleBindingModel
    {
        [Display(Name = "Aluminum Weight")]
        public Decimal AluminumWeight { get; set; }
        public Decimal GlassWeight { get; set; }
        public Decimal StandardPlastic { get; set; }
        public Decimal MiscPlastic { get; set; }
        [Required]
        [Display(Name = "Date of actual pickup")]
        public DateTime PickupDate { get; set; }
        [Required]
        [Display(Name = "Total Number of Bags")]
        public Decimal TotalBags { get; set; }
        public string Notes { get; set; }
        [Required]
        [Display(Name = "Community Name")]
        public string CommunityName { get; set; }
    }
}