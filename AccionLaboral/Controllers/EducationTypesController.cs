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
using System.Web.Http.Cors;

namespace AccionLaboral.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EducationTypesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public EducationTypesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/EducationTypes
        public IQueryable<EducationType> GetEducationTypes()
        {
            return db.EducationTypes;
        }

        // GET api/EducationTypes/5
        [ResponseType(typeof(EducationType))]
        public IHttpActionResult GetEducationType(int id)
        {
            EducationType educationtype = db.EducationTypes.Find(id);
            if (educationtype == null)
            {
                return NotFound();
            }

            return Ok(educationtype);
        }

        // PUT api/EducationTypes/5
        public IHttpActionResult PutEducationType(int id, EducationType educationtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != educationtype.EducationTypeId)
            {
                return BadRequest();
            }

            db.Entry(educationtype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationTypeExists(id))
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

        // POST api/EducationTypes
        [ResponseType(typeof(EducationType))]
        public IHttpActionResult PostEducationType(EducationType educationtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EducationTypes.Add(educationtype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = educationtype.EducationTypeId }, educationtype);
        }

        // DELETE api/EducationTypes/5
        [ResponseType(typeof(EducationType))]
        public IHttpActionResult DeleteEducationType(int id)
        {
            EducationType educationtype = db.EducationTypes.Find(id);
            if (educationtype == null)
            {
                return NotFound();
            }

            db.EducationTypes.Remove(educationtype);
            db.SaveChanges();

            return Ok(educationtype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducationTypeExists(int id)
        {
            return db.EducationTypes.Count(e => e.EducationTypeId == id) > 0;
        }
    }
}