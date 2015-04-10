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
    public class ReferencesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public ReferencesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/References
        public IQueryable<Reference> GetReferences()
        {
            return db.References;
        }

        // GET api/References/5
        [ResponseType(typeof(Reference))]
        public IHttpActionResult GetReference(int id)
        {
            Reference reference = db.References.Find(id);
            if (reference == null)
            {
                return NotFound();
            }

            return Ok(reference);
        }

        // PUT api/References/5
        public IHttpActionResult PutReference(int id, Reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reference.ReferenceId)
            {
                return BadRequest();
            }

            db.Entry(reference).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(id))
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

        // POST api/References
        [ResponseType(typeof(Reference))]
        public IHttpActionResult PostReference(Reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.References.Add(reference);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reference.ReferenceId }, reference);
        }

        // DELETE api/References/5
        [ResponseType(typeof(Reference))]
        public IHttpActionResult DeleteReference(int id)
        {
            Reference reference = db.References.Find(id);
            if (reference == null)
            {
                return NotFound();
            }

            db.References.Remove(reference);
            db.SaveChanges();

            return Ok(reference);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReferenceExists(int id)
        {
            return db.References.Count(e => e.ReferenceId == id) > 0;
        }
    }
}