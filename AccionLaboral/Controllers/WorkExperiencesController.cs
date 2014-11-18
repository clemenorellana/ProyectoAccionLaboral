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
    public class WorkExperiencesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/WorkExperiences
        public IQueryable<WorkExperience> GetWorkExperiences()
        {
            return db.WorkExperiences;
        }

        // GET api/WorkExperiences/5
        [ResponseType(typeof(WorkExperience))]
        public IHttpActionResult GetWorkExperience(int id)
        {
            WorkExperience workexperience = db.WorkExperiences.Find(id);
            if (workexperience == null)
            {
                return NotFound();
            }

            return Ok(workexperience);
        }

        // PUT api/WorkExperiences/5
        public IHttpActionResult PutWorkExperience(int id, WorkExperience workexperience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workexperience.WorkExperienceId)
            {
                return BadRequest();
            }

            db.Entry(workexperience).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExperienceExists(id))
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

        // POST api/WorkExperiences
        [ResponseType(typeof(WorkExperience))]
        public IHttpActionResult PostWorkExperience(WorkExperience workexperience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkExperiences.Add(workexperience);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workexperience.WorkExperienceId }, workexperience);
        }

        // DELETE api/WorkExperiences/5
        [ResponseType(typeof(WorkExperience))]
        public IHttpActionResult DeleteWorkExperience(int id)
        {
            WorkExperience workexperience = db.WorkExperiences.Find(id);
            if (workexperience == null)
            {
                return NotFound();
            }

            db.WorkExperiences.Remove(workexperience);
            db.SaveChanges();

            return Ok(workexperience);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkExperienceExists(int id)
        {
            return db.WorkExperiences.Count(e => e.WorkExperienceId == id) > 0;
        }
    }
}