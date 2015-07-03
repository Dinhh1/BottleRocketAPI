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
    public class PickupManager
    {
        #region ScheduledPickup Methods
        /// <summary>
        /// Search for a all scheduled pickups for a given user
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>StatusResult</returns>
        public static StatusResult<List<ScheduledPickup>> GetScheduledPickupsForUser(string userId)
        {
            List<ScheduledPickup> query = null;
            try
            {
                var db = ScheduledPickupDbContext.Create();
                query = (from p in db.ScheduledPickups
                         where p.UserId == userId
                         select p).ToList();

                if (query == null || !query.Any())
                {
                    return StatusResult<List<ScheduledPickup>>.Error("No results found");
                }
            }
            catch (Exception ex)
            {
                    return StatusResult<List<ScheduledPickup>>.Error(ex.Message);
            }
            return StatusResult<List<ScheduledPickup>>.Success(query);  
        }

        /// <summary>
        /// Checks if the user has a pickup already scheduled
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>bool</returns>
        private static bool CanRequestPickup(string userId)
        {
            try
            {
                var db = ScheduledPickupDbContext.Create();
                // check if the user has any pending pickups
                var query = (from p in db.ScheduledPickups
                            where p.UserId == userId && p.IsPickedUp == false
                            select p).FirstOrDefault();
                if (query != null)
                {
                    // user has a pending pickup, do not schedule another one
                    return false;
                }
            }
            catch (Exception ex)
            {
                // silent exception
                return false;
            }
            return true;
        }

        /// <summary>
        /// Schedules a pickup for the user. A user cannot have 2 pending pickups, this method ensures this is true.
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>StatusResult</returns>
        public static StatusResult<ScheduledPickup> SchedulePickup(string userId)
        {
            StatusResult<ScheduledPickup> result = null;
            try
            {
                // check if the user can request a pickup before inserting
                if (CanRequestPickup(userId))
                {
                    result = InsertScheduledPickup(userId);
                }
                else
                {
                    return StatusResult<ScheduledPickup>.Error("User aleady have a pickup scheduled");
                }
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Schedules a pickup for the user Asyncronously. A user cannot have 2 pending pickups, this method ensures this is true.
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<ScheduledPickup>> SchedulePickupAsync(string userId)
        {
            StatusResult<ScheduledPickup> result = null;
            try
            {
                // check if the user can request a pickup before inserting
                if (CanRequestPickup(userId))
                {
                    result = await InsertScheduledPickupAsync(userId);
                }
                else
                {
                    return StatusResult<ScheduledPickup>.Error("User aleady have a pickup scheduled");
                }
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Insert a pickup for user into db.  This method should only be used to manully insert
        /// into the db. Use SchedulePickup() to actually schedule a pickup
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>StatusResult</returns>
        public static StatusResult<ScheduledPickup> InsertScheduledPickup(string userId)
        {
            // get the user's address from db
            var userAddy = UserAddressManager.GetUserAddressByUserId(userId);

            if (userAddy.Code != StatusCode.OK)
            {
                return StatusResult<ScheduledPickup>.Error("User address not found");
            }

            var sp = new ScheduledPickup()
            {
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                IsPickedUp = false,
                AddressId = userAddy.Result.Id
            };
            return InsertScheduledPickup(sp);
        }

        /// <summary>
        /// Insert a pickup for user into db. This method should only be used to manully insert
        /// into the db. Use SchedulePickup() to actually schedule a pickup
        /// </summary>
        /// <param name="p">ScheduledPickup object properly populated</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<ScheduledPickup> InsertScheduledPickup(ScheduledPickup p)
        {
            try
            {
                var db = ScheduledPickupDbContext.Create();
                db.ScheduledPickups.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return StatusResult<ScheduledPickup>.Success();
        }

        /// <summary>
        /// Insert a pickup for user into db asyncronously. This method should only be used to manully insert
        /// into the db. Use SchedulePickup() to actually schedule a pickup
        /// </summary>
        /// <param name="userId">The user's Id </param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<ScheduledPickup>> InsertScheduledPickupAsync(string userId)
        {
            // get the user's address from db
            var userAddy = UserAddressManager.GetUserAddressByUserId(userId);

            if (userAddy.Code != StatusCode.OK)
            {
                return StatusResult<ScheduledPickup>.Error("User address not found");
            }

            var sp = new ScheduledPickup()
            {
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                IsPickedUp = false,
                AddressId = userAddy.Result.Id
            };
            return await InsertScheduledPickupAsync(sp);
        }

        /// <summary>
        /// Insert a pickup for user into db asyncronously, this method should only be used to manully insert
        /// into the db. Use SchedulePickup() to actually schedule a pickup
        /// </summary>
        /// <param name="p">ScheduledPickup object properly populated</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<ScheduledPickup>> InsertScheduledPickupAsync(ScheduledPickup p)
        {
            try
            {
                var db = ScheduledPickupDbContext.Create();
                db.ScheduledPickups.Add(p);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return StatusResult<ScheduledPickup>.Success();
        }

        public static async Task<StatusResult<ScheduledPickup>> UpdateScheduledPickupAsync(ScheduledPickup p)
        {
            //TODO:: NEED TO BE TESTED, NOT SURE IF THIS WORKS
            try
            {
                var db = ScheduledPickupDbContext.Create();
                db.Entry(p).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return StatusResult<ScheduledPickup>.Success();
        }

        public static StatusResult<ScheduledPickup> UpdateScheduledPickup(ScheduledPickup p)
        {
            //TODO:: NEED TO BE TESTED, NOT SURE IF THIS WORKS
            try
            {
                var db = ScheduledPickupDbContext.Create();
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult<ScheduledPickup>.Error(ex.Message);
            }
            return StatusResult<ScheduledPickup>.Success();
        }

        #endregion

        #region PickupReceipts  Methods

        /// <summary>
        /// Insert a pickup receipt asyncronously
        /// </summary>
        /// <param name="r">PickupReceipt object properly populated</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<PickupReceipt>> InsertPickupReceiptAsync(PickupReceipt r)
        {
            try
            {
                var db = PickupReceiptDbContext.Create();
                db.PickupReceipts.Add(r);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusResult<PickupReceipt>.Error(ex.Message);
            }
            return StatusResult<PickupReceipt>.Success();
        }

        /// <summary>
        /// Insert a pickup receipt 
        /// </summary>
        /// <param name="r">PickupReceipt object properly populated</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<PickupReceipt> InsertPickupReceipt(PickupReceipt r)
        {
            try
            {
                var db = PickupReceiptDbContext.Create();
                db.PickupReceipts.Add(r);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusResult<PickupReceipt>.Error(ex.Message);
            }
            return StatusResult<PickupReceipt>.Success();
        }

        #endregion
    }
}