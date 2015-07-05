using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BottleRocket.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace BottleRocket.BusinessLogic
{
    /// <summary>
    /// This class is used to abstract away all DB operations. For the most part, this class wraps calls and returns the appropriate status
    /// </summary>
    public class UserAddressManager
    {
        /// <summary>
        /// Add a user address asyncronously
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userId">The user's id</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<UserAddress>> InsertUserAddressAsync(RegisterBindingModel model, string userId)
        {
            return await InsertUserAddressAsync(UserAddress.CreateAddress(model, userId));
        }

        /// <summary>
        /// Add a user address asyncronously
        /// </summary>
        /// <param name="model">The UserAddress with appropriate data</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<UserAddress>> InsertUserAddressAsync(UserAddress a)
        {
            try
            {
                var db = BottleRocketDbContext.Create();
                db.UserAddresses.Add(a);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success(a);
        }

        /// <summary>
        /// Add a user address non-asyncronously
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userId">The user's id</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<UserAddress> InsertUserAddress(RegisterBindingModel model, string userId)
        {
            return InsertUserAddress(UserAddress.CreateAddress(model, userId));
        }

        /// <summary>
        /// Add a user address non-asyncronously
        /// </summary>
        /// <param name="model">The UserAddress with appropriate data</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<UserAddress> InsertUserAddress(UserAddress a)
        {
            try
            {
                var db = BottleRocketDbContext.Create();
                db.UserAddresses.Add(a);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success(a);
        }

        /// <summary>
        /// Search for a UserAddress by address id.
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<UserAddress> GetUserAddress(int id)
        {
            var query = BottleRocketDbContext.Create().UserAddresses.Find(id);
            if (query == null)
            {
                return StatusResult<UserAddress>.Error("No Results found");
            }
            return StatusResult<UserAddress>.Success(query);
        }

        /// <summary>
        /// Search for a UserAddress by address id asynchronously. This method returns null if not found
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static async Task<StatusResult<UserAddress>> GetUserAddressAsync(int id)
        {
            var query = await BottleRocketDbContext.Create().UserAddresses.FindAsync(id);
            if (query == null)
            {
                return StatusResult<UserAddress>.Error("No Results found");
            }
            return StatusResult<UserAddress>.Success(query);
        }

        /// <summary>
        /// Search for a UserAddress by User id. This method returns null if not found
        /// </summary>
        /// <param name="userId">The address of the provided userId</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static StatusResult<UserAddress> GetUserAddressByUserId(string userId)
        {
            UserAddress address = null;
            try
            {
                var db = BottleRocketDbContext.Create();
                // perform a query using linq
                address = (from addy in db.UserAddresses
                             where addy.UserId == userId
                             select addy).SingleOrDefault();
                if (address == null)
                {
                    return StatusResult<UserAddress>.Error("No Results found");
                }
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success(address);
        }

        /// <summary>
        /// Search for a UserAddress by User id async. This method returns null if not found
        /// </summary>
        /// <param name="userId">The address of the provided userId</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static async Task<StatusResult<UserAddress>> GetUserAddressByUserIdAsync(string userId)
        {
            UserAddress address = null;
            try
            {
                var db = BottleRocketDbContext.Create();
                // perform a query using linq
                address = await (from addy in db.UserAddresses
                           where addy.UserId == userId
                           select addy).SingleOrDefaultAsync();
                if (address == null)
                {
                    return StatusResult<UserAddress>.Error("No Results found");
                }
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success(address);
        }

    }
}