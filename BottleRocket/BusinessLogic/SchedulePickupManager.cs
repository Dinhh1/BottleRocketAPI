﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BottleRocket.Models;
using System.Threading.Tasks;

namespace BottleRocket.BusinessLogic
{
    public class SchedulePickupManager
    {
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
        public static bool CanRequestPickup(string userId)
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
        /// Insert a pickup for user into db
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
        /// Insert a pickup for user into db
        /// </summary>
        /// <param name="p">ScheduledPickup object properly populated</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<ScheduledPickup> InsertScheduledPickup(ScheduledPickup p)
        {
            try
            {
                // check if the user can request a pickup before inserting
                if (CanRequestPickup(p.UserId))
                {
                    var db = ScheduledPickupDbContext.Create();
                    db.ScheduledPickups.Add(p);
                    db.SaveChanges();
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
            return StatusResult<ScheduledPickup>.Success();
        }

        /// <summary>
        /// Insert a pickup for user into db asyncronously
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
        /// Insert a pickup for user into db asyncronously
        /// </summary>
        /// <param name="p">ScheduledPickup object properly populated</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<ScheduledPickup>> InsertScheduledPickupAsync(ScheduledPickup p)
        {
            try
            {
                if (CanRequestPickup(p.UserId))
                {
                    var db = ScheduledPickupDbContext.Create();
                    db.ScheduledPickups.Add(p);
                    await db.SaveChangesAsync();
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
            return StatusResult<ScheduledPickup>.Success();
        }
    }
}