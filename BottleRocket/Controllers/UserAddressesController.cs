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

namespace BottleRocket.Controllers
{
    public class UserAddressesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserAddresses
        public IQueryable<UserAddress> GetUserAddresses()
        {
            return db.UserAddresses;
        }

        // GET: api/UserAddresses/5
        [ResponseType(typeof(UserAddress))]
        public async Task<IHttpActionResult> GetUserAddress(int id)
        {
            UserAddress userAddress = await db.UserAddresses.FindAsync(id);
            if (userAddress == null)
            {
                return NotFound();
            }

            return Ok(userAddress);
        }

        // PUT: api/UserAddresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserAddress(int id, UserAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAddress.Id)
            {
                return BadRequest();
            }

            db.Entry(userAddress).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserAddresses
        [ResponseType(typeof(UserAddress))]
        public async Task<IHttpActionResult> PostUserAddress(UserAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserAddresses.Add(userAddress);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userAddress.Id }, userAddress);
        }

        // DELETE: api/UserAddresses/5
        [ResponseType(typeof(UserAddress))]
        public async Task<IHttpActionResult> DeleteUserAddress(int id)
        {
            UserAddress userAddress = await db.UserAddresses.FindAsync(id);
            if (userAddress == null)
            {
                return NotFound();
            }

            db.UserAddresses.Remove(userAddress);
            await db.SaveChangesAsync();

            return Ok(userAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAddressExists(int id)
        {
            return db.UserAddresses.Count(e => e.Id == id) > 0;
        }
    }
}