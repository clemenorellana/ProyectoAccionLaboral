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
    public class KnownProgramsController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/KnownPrograms
        public IQueryable<KnownProgram> GetKnownPrograms()
        {
            return db.KnownPrograms;
        }

        // GET api/KnownPrograms/5
        [ResponseType(typeof(KnownProgram))]
        public IHttpActionResult GetKnownProgram(int id)
        {
            KnownProgram knownprogram = db.KnownPrograms.Find(id);
            if (knownprogram == null)
            {
                return NotFound();
            }

            return Ok(knownprogram);
        }

        // PUT api/KnownPrograms/5
        public IHttpActionResult PutKnownProgram(int id, KnownProgram knownprogram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != knownprogram.KnownProgramId)
            {
                return BadRequest();
            }

            db.Entry(knownprogram).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnownProgramExists(id))
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

        // POST api/KnownPrograms
        [ResponseType(typeof(KnownProgram))]
        public IHttpActionResult PostKnownProgram(KnownProgram knownprogram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KnownPrograms.Add(knownprogram);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = knownprogram.KnownProgramId }, knownprogram);
        }

        // DELETE api/KnownPrograms/5
        [ResponseType(typeof(KnownProgram))]
        public IHttpActionResult DeleteKnownProgram(int id)
        {
            KnownProgram knownprogram = db.KnownPrograms.Find(id);
            if (knownprogram == null)
            {
                return NotFound();
            }

            db.KnownPrograms.Remove(knownprogram);
            db.SaveChanges();

            return Ok(knownprogram);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KnownProgramExists(int id)
        {
            return db.KnownPrograms.Count(e => e.KnownProgramId == id) > 0;
        }
    }
}