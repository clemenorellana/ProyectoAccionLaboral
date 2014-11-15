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
    public class ReferenceTypesController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/ReferenceTypes
        public IQueryable<ReferenceType> GetReferenceTypes()
        {
            return db.ReferenceTypes;
        }

        // GET api/ReferenceTypes/5
        [ResponseType(typeof(ReferenceType))]
        public IHttpActionResult GetReferenceType(int id)
        {
            ReferenceType referencetype = db.ReferenceTypes.Find(id);
            if (referencetype == null)
            {
                return NotFound();
            }

            return Ok(referencetype);
        }

        // PUT api/ReferenceTypes/5
        public IHttpActionResult PutReferenceType(int id, ReferenceType referencetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != referencetype.ReferenceTypeId)
            {
                return BadRequest();
            }

            db.Entry(referencetype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceTypeExists(id))
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

        // POST api/ReferenceTypes
        [ResponseType(typeof(ReferenceType))]
        public IHttpActionResult PostReferenceType(ReferenceType referencetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReferenceTypes.Add(referencetype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = referencetype.ReferenceTypeId }, referencetype);
        }

        // DELETE api/ReferenceTypes/5
        [ResponseType(typeof(ReferenceType))]
        public IHttpActionResult DeleteReferenceType(int id)
        {
            ReferenceType referencetype = db.ReferenceTypes.Find(id);
            if (referencetype == null)
            {
                return NotFound();
            }

            db.ReferenceTypes.Remove(referencetype);
            db.SaveChanges();

            return Ok(referencetype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReferenceTypeExists(int id)
        {
            return db.ReferenceTypes.Count(e => e.ReferenceTypeId == id) > 0;
        }
    }
}