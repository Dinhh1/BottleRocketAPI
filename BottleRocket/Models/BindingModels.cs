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

    public class SchedulePickupBindingModel
    {
        [Required]
        [Display(Name = "UserId")]
        public string UserId { get; set; }
    }

    public class PickupReceiptBindingModel
    {
        [Required]
        [Display(Name = "User's Id")]
        public string UserId {get;set;}

        [Required]
        public Decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Pickup Cycle's Id")]
        public int PickupCycleId { get; set; }

        [Required]
        [Display(Name = "Number of bags")]
        public Decimal BagCount { get; set; }

    }

    public class PickupReceiptResultBindingModel
    {
        public int MetricId { get; set; }
        public int ReceiptId { get; set; }
        public string UserId { get; set; }
        public Decimal BagCount { get; set; }
        public Decimal TotalAmount { get; set; }
        public int PickupCycleId { get; set; }
    }
}