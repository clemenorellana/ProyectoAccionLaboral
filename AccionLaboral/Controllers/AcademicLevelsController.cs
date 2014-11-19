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
    public class AcademicLevelsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/AcademicLevels
        public IQueryable<AcademicLevel> GetAcademicLevels()
        {
            return db.AcademicLevels;
        }

        // GET api/AcademicLevels/5
        [ResponseType(typeof(AcademicLevel))]
        public IHttpActionResult GetAcademicLevel(int id)
        {
            AcademicLevel academiclevel = db.AcademicLevels.Find(id);
            if (academiclevel == null)
            {
                return NotFound();
            }

            return Ok(academiclevel);
        }

        // PUT api/AcademicLevels/5
        public IHttpActionResult PutAcademicLevel(int id, AcademicLevel academiclevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != academiclevel.AcademicLevelId)
            {
                return BadRequest();
            }

            db.Entry(academiclevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicLevelExists(id))
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

        // POST api/AcademicLevels
        [ResponseType(typeof(AcademicLevel))]
        public IHttpActionResult PostAcademicLevel(AcademicLevel academiclevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AcademicLevels.Add(academiclevel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = academiclevel.AcademicLevelId }, academiclevel);
        }

        // DELETE api/AcademicLevels/5
        [ResponseType(typeof(AcademicLevel))]
        public IHttpActionResult DeleteAcademicLevel(int id)
        {
            AcademicLevel academiclevel = db.AcademicLevels.Find(id);
            if (academiclevel == null)
            {
                return NotFound();
            }

            db.AcademicLevels.Remove(academiclevel);
            db.SaveChanges();

            return Ok(academiclevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcademicLevelExists(int id)
        {
            return db.AcademicLevels.Count(e => e.AcademicLevelId == id) > 0;
        }
    }
}