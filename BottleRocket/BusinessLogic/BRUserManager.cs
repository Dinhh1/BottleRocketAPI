using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BottleRocket.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BottleRocket.BusinessLogic
{
    public class BRUserManager
    {
        /// <summary>
        /// Registers a new user and also ensures that address is added into the db properly asyncronously
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userManager">The UserManager context to perform the add</param>
        /// <returns>StatusResult</returns>
        public static async Task<StatusResult<UserInfoViewModel>> RegisterUserAsync(RegisterBindingModel model, ApplicationUserManager userManager)
        {
            UserInfoViewModel obj = null;
            try
            {
                var user = ApplicationUser.CreateUser(model);
                IdentityResult userResult = await userManager.CreateAsync(user, model.Password);

                if (!userResult.Succeeded)
                {
                    return StatusResult<UserInfoViewModel>.Error();
                }

                // add the address
                var addressResult = await UserAddressManager.InsertUserAddressAsync(model, user.Id);

                if (addressResult.Code != StatusCode.OK)
                {
                    //TODO:: Need to handle error where the address was not added successfully, can't find anything to rollback CreateAsync,
                    // it seems like CreateAsync is set to autocommit or something. 
                    // This is a little quick and dirty and hacky, but here i am just going to delete the user if we dont succeed with adding the address
                    await userManager.DeleteAsync(user);
                    return StatusResult<UserInfoViewModel>.Error(addressResult.Message);
                }
                obj = new UserInfoViewModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                return StatusResult<UserInfoViewModel>.Error(ex.Message);
            }
            return StatusResult<UserInfoViewModel>.Success(obj);
        }

        /// <summary>
        /// Registers a new user and also ensures that address is added into the db properly
        /// </summary>
        /// <param name="model">The RegisterBindingModel with appropriate data</param>
        /// <param name="userManager">The UserManager context to perform the add</param>
        /// <returns>StatusResult</returns>
        public static StatusResult<UserInfoViewModel> RegisterUser(RegisterBindingModel model, ApplicationUserManager userManager)
        {
            UserInfoViewModel obj = null;
            try
            {
                var user = ApplicationUser.CreateUser(model);
                IdentityResult userResult = userManager.Create(user, model.Password);

                if (!userResult.Succeeded)
                {
                    return StatusResult<UserInfoViewModel>.Error();
                }

                model.Address = null;
                // add the address
                var addressResult = UserAddressManager.InsertUserAddress(model, user.Id);

                if (addressResult.Code != StatusCode.OK)
                {
                    //TODO:: Need to handle error where the address was not added successfully, can't find anything to rollback CreateAsync,
                    // it seems like CreateAsync is set to autocommit or something. 
                    // This is a little quick and dirty and hacky, but here i am just going to delete the user if we dont succeed with adding the address
                    userManager.Delete(user);
                    return StatusResult<UserInfoViewModel>.Error(addressResult.Message);
                }
                obj = new UserInfoViewModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                return StatusResult<UserInfoViewModel>.Error(ex.Message);
            }
            return StatusResult<UserInfoViewModel>.Success(obj);
        }

    }
}