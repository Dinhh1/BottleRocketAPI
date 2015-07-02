using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BottleRocket.Models;
using System.Threading.Tasks;

namespace BottleRocket.BusinessLogic
{
    /// <summary>
    /// This class is used to abstract away all DB operations. For the most part, this class wraps calls and returns the appropriate status
    /// </summary>
    public class UserAddressUtilities
    {

        /// <summary>
        /// Add a user address asyncronously
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userId">The user's id</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult> InsertUserAddressAsync(RegisterBindingModel model, string userId)
        {
            return await InsertUserAddressAsync(UserAddress.CreateAddress(model, userId));
        }

        /// <summary>
        /// Add a user address asyncronously
        /// </summary>
        /// <param name="model">The UserAddress with appropriate data</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult> InsertUserAddressAsync(UserAddress a)
        {
            try
            {
                var db = UserAddressesDbContext.Create();
                db.UserAddresses.Add(a);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult.Error(ex.Message);
            }
            return StatusResult.Success();
        }

        /// <summary>
        /// Add a user address non-asyncronously
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userId">The user's id</param>
        /// <returns>StatusResult</returns>
        public static StatusResult InsertUserAddress(RegisterBindingModel model, string userId)
        {
            return InsertUserAddress(UserAddress.CreateAddress(model, userId));
        }

        /// <summary>
        /// Add a user address non-asyncronously
        /// </summary>
        /// <param name="model">The UserAddress with appropriate data</param>
        /// <returns>StatusResult</returns>
        public static StatusResult InsertUserAddress(UserAddress a)
        {
            try
            {
                var db = UserAddressesDbContext.Create();
                db.UserAddresses.Add(a);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult.Error(ex.Message);
            }
            return StatusResult.Success();
        }

        /// <summary>
        /// Search for a UserAddress by address id. This method returns null if not found
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static UserAddress GetUserAddress(int id)
        {
            return UserAddressesDbContext.Create().UserAddresses.Find(id);
        }

        /// <summary>
        /// Search for a UserAddress by address id asynchronously. This method returns null if not found
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static async Task<UserAddress> GetUserAddressAsync(int id)
        {
            return await UserAddressesDbContext.Create().UserAddresses.FindAsync(id);
        }

        /// <summary>
        /// Search for a UserAddress by User id. This method returns null if not found
        /// </summary>
        /// <param name="userId">The address of the provided userId</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static UserAddress GetUserAddressByUserId(string userId)
        {
            UserAddress address = null;
            try
            {
                var db = UserAddressesDbContext.Create();
                // perform a query using linq
                address = (from addy in db.UserAddresses
                             where addy.UserId == userId
                             select addy).SingleOrDefault();
            }
            catch (Exception ex)
            {
                // silent exception
                return null;
            }

            return address;
        }

    }
}