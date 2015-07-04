using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;

namespace BottleRocket.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public ApplicationUser()
        {
            // empty constructor
        }
        public ApplicationUser(RegisterBindingModel model)
        {
            UserName = model.Email;
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            DateCreated = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }

        public static ApplicationUser CreateUser(RegisterBindingModel model)
        {
            var user = new ApplicationUser(model);
            return user;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}