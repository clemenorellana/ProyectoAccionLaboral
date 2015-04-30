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
using AccionLaboral.Helpers.Filters;


namespace AccionLaboral.Controllers
{
    [Authorize]
    public class CompaniesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public CompaniesController()
        {
            db.Database.CommandTimeout = 180;
        }

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

        //-------------------------------------------Companies Report------------------------------------------

        private List<Company> getCompaniesData(CompaniesFilters id)
        {
            List<Company> companies = new List<Company>();

            DateTime dtDateFrom = id.DateFrom;
            TimeSpan tsDateFrom = new TimeSpan(0, 0, 0);
            id.DateFrom = dtDateFrom.Date + tsDateFrom;

            DateTime dtDateTo = id.DateTo;
            TimeSpan tsDateTo = new TimeSpan(0, 0, 0);
            id.DateTo = dtDateTo.Date + tsDateTo;

            List<Company> companiesTemp = db.Companies.Include(r => r.ContactsByCompany).Include(r => r.VacantsByCompany).ToList();
            foreach (Company c in companiesTemp)
            {
                DateTime date = c.DateCreated;
                TimeSpan ts = new TimeSpan(0, 0, 0);
                date = date.Date + ts;
                c.DateCreated = date;
                companies.Add(c);
            }

            if (id.DateFrom.Year > 1 && id.DateTo.Year == 1)
            {
                companies = companies.Where(r => r.DateCreated >= id.DateFrom).ToList();
            }
            else if (id.DateFrom.Year == 1 && id.DateTo.Year > 1)
            {
                companies = companies.Where(r => r.DateCreated <= id.DateTo).ToList();

            }
            else if (id.DateFrom.Year > 1 && id.DateTo.Year > 1)
            {
                companies = companies.Where(r => r.DateCreated >= id.DateFrom && r.DateCreated <= id.DateTo).ToList();

            }
            return companies;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/companiesdatareport/")]
        public IHttpActionResult CompaniesDataReport(CompaniesFilters id)
        {
            List<Company> companies = new List<Company>();
            companies = getCompaniesData(id);

            return Ok(companies);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/exportcompaniesreport")]
        public HttpResponseMessage ExportCompaniesReport(CompaniesFilters id)
        {
            CompaniesFilters filters = id;
            filters.Companies = getCompaniesData(id);
            
            try
            {
                if (filters != null)
                {
                    string filename = id.ReportName + ".xls";
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string documentPath = path + "Reports\\" + filename;
                    Reports.Helpers.Companies.GenerateReport(filename, filters);
                }
                return Request.CreateResponse<CompaniesFilters>(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte");
            }
        }

    }
}