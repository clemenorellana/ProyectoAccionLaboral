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
    public class TrackingsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Trackings
        public IQueryable<Tracking> GetTrackings()
        {
            return db.Trackings;
        }

        // GET api/Trackings/5
        [ResponseType(typeof(Tracking))]
        public IHttpActionResult GetClientTracking(int id)
        {
            Tracking clienttracking = db.Trackings.Find(id);
            if (clienttracking == null)
            {
                return NotFound();
            }

            return Ok(clienttracking);
        }

        // PUT api/Trackings/5
        public IHttpActionResult PutClientTracking(int id, Tracking clienttracking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clienttracking.TrackingId)
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

        // POST api/Trackings
        [ResponseType(typeof(Tracking))]
        public IHttpActionResult PostClientTracking(Tracking clienttracking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trackings.Add(clienttracking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clienttracking.TrackingId }, clienttracking);
        }

        // DELETE api/Trackings/5
        [ResponseType(typeof(Tracking))]
        public IHttpActionResult DeleteClientTracking(int id)
        {
            Tracking clienttracking = db.Trackings.Find(id);
            if (clienttracking == null)
            {
                return NotFound();
            }

            db.Trackings.Remove(clienttracking);
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
            return db.Trackings.Count(e => e.TrackingId == id) > 0;
        }
    }
}