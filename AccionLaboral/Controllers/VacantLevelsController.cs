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
    public class VacantLevelsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/VacantLevels
        public IQueryable<VacantLevel> GetVacantLevels()
        {
            return db.VacantLevels;
        }

        // GET api/VacantLevels/5
        [ResponseType(typeof(VacantLevel))]
        public IHttpActionResult GetVacantLevel(int id)
        {
            VacantLevel vacantlevel = db.VacantLevels.Find(id);
            if (vacantlevel == null)
            {
                return NotFound();
            }

            return Ok(vacantlevel);
        }

        // PUT api/VacantLevels/5
        public IHttpActionResult PutVacantLevel(int id, VacantLevel vacantlevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacantlevel.VacantLevelId)
            {
                return BadRequest();
            }

            db.Entry(vacantlevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacantLevelExists(id))
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

        // POST api/VacantLevels
        [ResponseType(typeof(VacantLevel))]
        public IHttpActionResult PostVacantLevel(VacantLevel vacantlevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VacantLevels.Add(vacantlevel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vacantlevel.VacantLevelId }, vacantlevel);
        }

        // DELETE api/VacantLevels/5
        [ResponseType(typeof(VacantLevel))]
        public IHttpActionResult DeleteVacantLevel(int id)
        {
            VacantLevel vacantlevel = db.VacantLevels.Find(id);
            if (vacantlevel == null)
            {
                return NotFound();
            }

            db.VacantLevels.Remove(vacantlevel);
            db.SaveChanges();

            return Ok(vacantlevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacantLevelExists(int id)
        {
            return db.VacantLevels.Count(e => e.VacantLevelId == id) > 0;
        }
    }
}