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
    public class SchedulePickupController : ApiController
    {

        // POST: api/SchedulePickup
        public async Task<IHttpActionResult> SchedulePickup(SchedulePickupBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(StatusResult<ScheduledPickup>.Error("ModelState is Invalid"));
            }

            var response = await SchedulePickupManager.InsertScheduledPickupAsync(model.UserId);
            return Ok(response);
        }

    }
}