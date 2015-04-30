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
    [Authorize]
    public class ContactsByCompanyController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public ContactsByCompanyController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/ContactsByCompany
        public IQueryable<ContactByCompany> GetContactByCompanies()
        {
            return db.ContactByCompanies;
        }

        // GET api/ContactsByCompany/5
        [ResponseType(typeof(ContactByCompany))]
        public IHttpActionResult GetContactByCompany(int id)
        {
            ContactByCompany contactbycompany = db.ContactByCompanies.Find(id);
            if (contactbycompany == null)
            {
                return NotFound();
            }

            return Ok(contactbycompany);
        }

        // PUT api/ContactsByCompany/5
        public IHttpActionResult PutContactByCompany(int id, ContactByCompany contactbycompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactbycompany.ContactByCompanyId)
            {
                return BadRequest();
            }

            db.Entry(contactbycompany).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactByCompanyExists(id))
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

        // POST api/ContactsByCompany
        [ResponseType(typeof(ContactByCompany))]
        public IHttpActionResult PostContactByCompany(ContactByCompany contactbycompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactByCompanies.Add(contactbycompany);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactbycompany.ContactByCompanyId }, contactbycompany);
        }

        // DELETE api/ContactsByCompany/5
        [ResponseType(typeof(ContactByCompany))]
        public IHttpActionResult DeleteContactByCompany(int id)
        {
            ContactByCompany contactbycompany = db.ContactByCompanies.Find(id);
            if (contactbycompany == null)
            {
                return NotFound();
            }

            db.ContactByCompanies.Remove(contactbycompany);
            db.SaveChanges();

            return Ok(contactbycompany);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactByCompanyExists(int id)
        {
            return db.ContactByCompanies.Count(e => e.ContactByCompanyId == id) > 0;
        }
    }
}