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
    public class CompaniesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Companies
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Companies.Include(r => r.ContactsByCompany).First(r => r.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT api/Companies/5
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            var dbCompany = db.Companies
                                .Include(r => r.ContactsByCompany)
                                .Single(r => r.CompanyId == id);

            db.Entry(dbCompany).CurrentValues.SetValues(company);

            foreach (var dbContact in dbCompany.ContactsByCompany.ToList())
            {
                if (!company.ContactsByCompany.Any(x => x.ContactByCompanyId == dbContact.ContactByCompanyId))
                    db.ContactByCompanies.Remove(dbContact);
            }

            foreach (var contact in company.ContactsByCompany)
            {
                contact.CompanyId = company.CompanyId;
                if (contact.ContactByCompanyId != 0)
                {
                    var dbContact = dbCompany.ContactsByCompany.SingleOrDefault(x => x.ContactByCompanyId == contact.ContactByCompanyId);
                    db.Entry(dbContact).CurrentValues.SetValues(contact);
                }
                else 
                {
                    dbCompany.ContactsByCompany.Add(contact);
                }
            }


            //db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            company.DateCreated = DateTime.Now;
            db.Companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
        }

        // DELETE api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.CompanyId == id) > 0;
        }
    }
}