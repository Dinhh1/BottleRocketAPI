using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BottleRocket.Models
{
    [Table("UserAddresses")]
    public class UserAddress
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public UserAddress()
        {
            // empty constructor
        }

        public UserAddress(RegisterBindingModel model, string userId)
        {
            Address = model.Address;
            City = model.City;
            State = model.State;
            ZipCode = model.ZipCode;
            UserId = userId;
            DateCreated = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a UserAddress Object
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userId">The user's id</param>
        /// <returns>UserAddress</returns>
        public static UserAddress CreateAddress(RegisterBindingModel model, string userId)
        {
            return new UserAddress(model, userId);
        }
    }

    public class UserAddressesDbContext : DbContext
    {
        public DbSet<UserAddress> UserAddresses { get; set; }
        public UserAddressesDbContext()
            : base("DefaultConnection")
        {
        }
        public static UserAddressesDbContext Create()
        {
            return new UserAddressesDbContext();
        }
    }


}