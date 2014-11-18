using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto_AccionLaboral.Models;

namespace Proyecto_AccionLaboral.Controllers
{
    public class ClientTrackingsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/ClientTrackings
        public IQueryable<ClientTracking> GetClientTrackings()
        {
            return db.ClientTrackings;
        }

        // GET api/ClientTrackings/5
        [ResponseType(typeof(ClientTracking))]
        public IHttpActionResult GetClientTracking(int id)
        {
            ClientTracking clienttracking = db.ClientTrackings.Find(id);
            if (clienttracking == null)
            {
                return NotFound();
            }

            return Ok(clienttracking);
        }

        // PUT api/ClientTrackings/5
        public IHttpActionResult PutClientTracking(int id, ClientTracking clienttracking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clienttracking.ClientTrackingId)
            {
                return BadRequest();
            }

            db.Entry(clienttracking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientTrackingExists(id))
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

        // POST api/ClientTrackings
        [ResponseType(typeof(ClientTracking))]
        public IHttpActionResult PostClientTracking(ClientTracking clienttracking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientTrackings.Add(clienttracking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clienttracking.ClientTrackingId }, clienttracking);
        }

        // DELETE api/ClientTrackings/5
        [ResponseType(typeof(ClientTracking))]
        public IHttpActionResult DeleteClientTracking(int id)
        {
            ClientTracking clienttracking = db.ClientTrackings.Find(id);
            if (clienttracking == null)
            {
                return NotFound();
            }

            db.ClientTrackings.Remove(clienttracking);
            db.SaveChanges();

            return Ok(clienttracking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientTrackingExists(int id)
        {
            return db.ClientTrackings.Count(e => e.ClientTrackingId == id) > 0;
        }
    }
}