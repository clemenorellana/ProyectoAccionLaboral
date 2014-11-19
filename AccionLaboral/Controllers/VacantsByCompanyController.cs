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
    public class VacantsByCompanyController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/VacantsByCompany
        public IQueryable<VacantByCompany> GetVacantByCompanies()
        {
            return db.VacantByCompanies;
        }

        // GET api/VacantsByCompany/5
        [ResponseType(typeof(VacantByCompany))]
        public IHttpActionResult GetVacantByCompany(int id)
        {
            VacantByCompany vacantbycompany = db.VacantByCompanies.Find(id);
            if (vacantbycompany == null)
            {
                return NotFound();
            }

            return Ok(vacantbycompany);
        }

        // PUT api/VacantsByCompany/5
        public IHttpActionResult PutVacantByCompany(int id, VacantByCompany vacantbycompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacantbycompany.VacantByCompanyId)
            {
                return BadRequest();
            }

            db.Entry(vacantbycompany).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacantByCompanyExists(id))
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

        // POST api/VacantsByCompany
        [ResponseType(typeof(VacantByCompany))]
        public IHttpActionResult PostVacantByCompany(VacantByCompany vacantbycompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VacantByCompanies.Add(vacantbycompany);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vacantbycompany.VacantByCompanyId }, vacantbycompany);
        }

        // DELETE api/VacantsByCompany/5
        [ResponseType(typeof(VacantByCompany))]
        public IHttpActionResult DeleteVacantByCompany(int id)
        {
            VacantByCompany vacantbycompany = db.VacantByCompanies.Find(id);
            if (vacantbycompany == null)
            {
                return NotFound();
            }

            db.VacantByCompanies.Remove(vacantbycompany);
            db.SaveChanges();

            return Ok(vacantbycompany);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacantByCompanyExists(int id)
        {
            return db.VacantByCompanies.Count(e => e.VacantByCompanyId == id) > 0;
        }
    }
}