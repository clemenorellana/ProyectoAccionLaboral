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
    public class KnownLanguagesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public KnownLanguagesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/KnownLanguages
        public IQueryable<KnownLanguage> GetKnownLanguages()
        {
            return db.KnownLanguages;
        }

        // GET api/KnownLanguages/5
        [ResponseType(typeof(KnownLanguage))]
        public IHttpActionResult GetKnownLanguage(int id)
        {
            KnownLanguage knownlanguage = db.KnownLanguages.Find(id);
            if (knownlanguage == null)
            {
                return NotFound();
            }

            return Ok(knownlanguage);
        }

        // PUT api/KnownLanguages/5
        public IHttpActionResult PutKnownLanguage(int id, KnownLanguage knownlanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != knownlanguage.KnownLanguageId)
            {
                return BadRequest();
            }

            db.Entry(knownlanguage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnownLanguageExists(id))
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

        // POST api/KnownLanguages
        [ResponseType(typeof(KnownLanguage))]
        public IHttpActionResult PostKnownLanguage(KnownLanguage knownlanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KnownLanguages.Add(knownlanguage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = knownlanguage.KnownLanguageId }, knownlanguage);
        }

        // DELETE api/KnownLanguages/5
        [ResponseType(typeof(KnownLanguage))]
        public IHttpActionResult DeleteKnownLanguage(int id)
        {
            KnownLanguage knownlanguage = db.KnownLanguages.Find(id);
            if (knownlanguage == null)
            {
                return NotFound();
            }

            db.KnownLanguages.Remove(knownlanguage);
            db.SaveChanges();

            return Ok(knownlanguage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KnownLanguageExists(int id)
        {
            return db.KnownLanguages.Count(e => e.KnownLanguageId == id) > 0;
        }
    }
}