using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BottleRocket.Models;
using BottleRocket.BusinessLogic;

namespace BottleRocket.Controllers
{
    //[Authorize]
   [RoutePrefix("api/Pickup")]
    public class PickupController : ApiController
    {

        [Route("Cycle")]

       // POST: api/Pickup/Cycle
        public async Task<IHttpActionResult> CreatePickupCycle(PickupCycleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok<StatusResult<PickupCycle>>(StatusResult<PickupCycle>.Error("Model is Invalid"));
            }
            var response = await PickupManager.CreatePickupCycleAsync(model);
            return Ok(response);
        }


        [Route("Schedule")]
        // POST: api/Pickup/Schedule
        public async Task<IHttpActionResult> SchedulePickup(SchedulePickupBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(StatusResult<ScheduledPickup>.Error("Model is Invalid"));
            }

            var response = await PickupManager.SchedulePickupAsync(model.UserId);
            return Ok(response);
        }

    }
}