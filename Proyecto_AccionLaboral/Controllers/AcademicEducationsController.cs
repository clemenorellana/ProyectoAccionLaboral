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
    public class AcademicEducationsController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/AcademicEducations
        public IQueryable<AcademicEducation> GetAcademicEducations()
        {
            return db.AcademicEducations;
        }

        // GET api/AcademicEducations/5
        [ResponseType(typeof(AcademicEducation))]
        public IHttpActionResult GetAcademicEducation(int id)
        {
            AcademicEducation academiceducation = db.AcademicEducations.Find(id);
            if (academiceducation == null)
            {
                return NotFound();
            }

            return Ok(academiceducation);
        }

        // PUT api/AcademicEducations/5
        public IHttpActionResult PutAcademicEducation(int id, AcademicEducation academiceducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != academiceducation.AcademicEducationId)
            {
                return BadRequest();
            }

            db.Entry(academiceducation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicEducationExists(id))
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

        // POST api/AcademicEducations
        [ResponseType(typeof(AcademicEducation))]
        public IHttpActionResult PostAcademicEducation(AcademicEducation academiceducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AcademicEducations.Add(academiceducation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = academiceducation.AcademicEducationId }, academiceducation);
        }

        // DELETE api/AcademicEducations/5
        [ResponseType(typeof(AcademicEducation))]
        public IHttpActionResult DeleteAcademicEducation(int id)
        {
            AcademicEducation academiceducation = db.AcademicEducations.Find(id);
            if (academiceducation == null)
            {
                return NotFound();
            }

            db.AcademicEducations.Remove(academiceducation);
            db.SaveChanges();

            return Ok(academiceducation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcademicEducationExists(int id)
        {
            return db.AcademicEducations.Count(e => e.AcademicEducationId == id) > 0;
        }
    }
}