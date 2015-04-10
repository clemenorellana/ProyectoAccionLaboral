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
using AccionLaboral.Models;

namespace AccionLaboral.Controllers
{
    public class TrackingsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public TrackingsController()
        {
            db.Database.CommandTimeout = 180;
        }

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
        public IHttpActionResult PutClientTracking(int id, Client clienttracking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clienttracking.Trackings.ToList()[0].TrackingId)
            {
                return BadRequest();
            }

            var dbClients = db.Clients.Include(x => x.Trackings)
                      .Single(c => c.ClientId == clienttracking.ClientId);
            db.Entry(dbClients).CurrentValues.SetValues(clienttracking);


            var dbTrackings = db.Trackings
                .Include(x => x.TrackingDetails)
                       .Single(c => c.ClientId == clienttracking.ClientId);
            db.Entry(dbTrackings).CurrentValues.SetValues(clienttracking.Trackings);
            //TrackingDetails
            foreach (var dbTrackingDetail in dbTrackings.TrackingDetails.ToList())
                if (!clienttracking.Trackings.ToList()[0].TrackingDetails.Any(s => s.TrackingDetailId == dbTrackingDetail.TrackingDetailId))
                    db.TrackingDetails.Remove(dbTrackingDetail);

            foreach (var newTrackingDetail in clienttracking.Trackings.ToList()[0].TrackingDetails)
            {
                //newTrackingDetail.TrackingDetailId = clienttracking.Trackings.ToList()[0].TrackingId;
                if (newTrackingDetail.TrackingDetailId != 0)
                {
                    var dbTrackingDetail = dbTrackings.TrackingDetails.SingleOrDefault(s => s.TrackingDetailId == newTrackingDetail.TrackingDetailId);
                    db.Entry(dbTrackingDetail).CurrentValues.SetValues(newTrackingDetail);
                }
                else
                {
                    newTrackingDetail.Date = DateTime.Now;
                    dbTrackings.TrackingDetails.Add(newTrackingDetail);
                }

            }

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