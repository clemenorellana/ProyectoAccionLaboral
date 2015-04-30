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
    public class LanguageLevelsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public LanguageLevelsController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/LanguageLevels
        public IQueryable<LanguageLevel> GetLanguageLevels()
        {
            return db.LanguageLevels;
        }

        // GET api/LanguageLevels/5
        [ResponseType(typeof(LanguageLevel))]
        public IHttpActionResult GetLanguageLevel(int id)
        {
            LanguageLevel languagelevel = db.LanguageLevels.Find(id);
            if (languagelevel == null)
            {
                return NotFound();
            }

            return Ok(languagelevel);
        }

        // PUT api/LanguageLevels/5
        public IHttpActionResult PutLanguageLevel(int id, LanguageLevel languagelevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != languagelevel.LanguageLevelId)
            {
                return BadRequest();
            }

            db.Entry(languagelevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageLevelExists(id))
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

        // POST api/LanguageLevels
        [ResponseType(typeof(LanguageLevel))]
        public IHttpActionResult PostLanguageLevel(LanguageLevel languagelevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LanguageLevels.Add(languagelevel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = languagelevel.LanguageLevelId }, languagelevel);
        }

        // DELETE api/LanguageLevels/5
        [ResponseType(typeof(LanguageLevel))]
        public IHttpActionResult DeleteLanguageLevel(int id)
        {
            LanguageLevel languagelevel = db.LanguageLevels.Find(id);
            if (languagelevel == null)
            {
                return NotFound();
            }

            db.LanguageLevels.Remove(languagelevel);
            db.SaveChanges();

            return Ok(languagelevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageLevelExists(int id)
        {
            return db.LanguageLevels.Count(e => e.LanguageLevelId == id) > 0;
        }
    }
}