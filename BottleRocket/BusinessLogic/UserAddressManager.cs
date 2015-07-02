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
                var db = UserAddressesDbContext.Create();
                db.UserAddresses.Add(a);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success();
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
                var db = UserAddressesDbContext.Create();
                db.UserAddresses.Add(a);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult<UserAddress>.Error(ex.Message);
            }
            return StatusResult<UserAddress>.Success();
        }

        /// <summary>
        /// Search for a UserAddress by address id. This method returns null if not found
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static StatusResult<UserAddress> GetUserAddress(int id)
        {
            var query = UserAddressesDbContext.Create().UserAddresses.Find(id);
            if (query == null)
            {
                return StatusResult<UserAddress>.Error("No Result(s) found");
            }
            else
            {
                return StatusResult<UserAddress>.Success(query);
            }
        }

        /// <summary>
        /// Search for a UserAddress by address id asynchronously. This method returns null if not found
        /// </summary>
        /// <param name="id">UserAddress object's id</param>
        /// <returns>UserAddress if found, NULL otherwise</returns>
        public static async Task<StatusResult<UserAddress>> GetUserAddressAsync(int id)
        {
            var query = await UserAddressesDbContext.Create().UserAddresses.FindAsync(id);
            if (query == null)
            {
                return StatusResult<UserAddress>.Error("No Result(s) found");
            }
            else
            {
                return StatusResult<UserAddress>.Success(query);
            }
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
                var db = UserAddressesDbContext.Create();
                // perform a query using linq
                address = (from addy in db.UserAddresses
                             where addy.UserId == userId
                             select addy).SingleOrDefault();
                if (address == null)
                {
                    return StatusResult<UserAddress>.Error("No Result(s) found");
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