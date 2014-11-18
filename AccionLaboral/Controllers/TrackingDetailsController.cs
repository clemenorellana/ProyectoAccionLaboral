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
    public class TrackingDetailsController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/TrackingDetails
        public IQueryable<TrackingDetail> GetTrackingDetails()
        {
            return db.TrackingDetails;
        }

        // GET api/TrackingDetails/5
        [ResponseType(typeof(TrackingDetail))]
        public IHttpActionResult GetTrackingDetail(int id)
        {
            TrackingDetail trackingdetail = db.TrackingDetails.Find(id);
            if (trackingdetail == null)
            {
                return NotFound();
            }

            return Ok(trackingdetail);
        }

        // PUT api/TrackingDetails/5
        public IHttpActionResult PutTrackingDetail(int id, TrackingDetail trackingdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trackingdetail.TrackingDetailId)
            {
                return BadRequest();
            }

            db.Entry(trackingdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackingDetailExists(id))
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

        // POST api/TrackingDetails
        [ResponseType(typeof(TrackingDetail))]
        public IHttpActionResult PostTrackingDetail(TrackingDetail trackingdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TrackingDetails.Add(trackingdetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trackingdetail.TrackingDetailId }, trackingdetail);
        }

        // DELETE api/TrackingDetails/5
        [ResponseType(typeof(TrackingDetail))]
        public IHttpActionResult DeleteTrackingDetail(int id)
        {
            TrackingDetail trackingdetail = db.TrackingDetails.Find(id);
            if (trackingdetail == null)
            {
                return NotFound();
            }

            db.TrackingDetails.Remove(trackingdetail);
            db.SaveChanges();

            return Ok(trackingdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrackingDetailExists(int id)
        {
            return db.TrackingDetails.Count(e => e.TrackingDetailId == id) > 0;
        }
    }
}