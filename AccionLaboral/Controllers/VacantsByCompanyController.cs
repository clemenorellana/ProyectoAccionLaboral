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
    public class VacantsByCompanyController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public VacantsByCompanyController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/VacantsByCompany
        public IQueryable<VacantByCompany> GetVacantByCompanies()
        {
            return db.VacantByCompanies.Include(r => r.AcademicLevel)
                                        .Include(r => r.Career)
                                        .Include(r => r.City)
                                        .Include(r => r.Company)
                                        .Include(r => r.VacantLevel)
                                        .Include(r => r.InterviewType);
        }

        // GET api/VacantsByCompany/5
        [ResponseType(typeof(VacantByCompany))]
        public IHttpActionResult GetVacantByCompany(int id)
        {
            VacantByCompany vacantbycompany = db.VacantByCompanies.Include(r => r.AcademicLevel)
                                        .Include(r => r.Career)
                                        .Include(r => r.City)
                                        .Include(r => r.Company)
                                        .Include(r => r.VacantLevel)
                                        .Include(r => r.InterviewType)
                                        .First(r => r.VacantByCompanyId == id);
                                        
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


        //-------------------------------------------Vacant Report------------------------------------------


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/VacantDataReport/")]
        public IHttpActionResult VacantDataReport(VacantFilter id)
        {
            List<VacantByCompany> vacantsByCompanies = new List<VacantByCompany>();

            DateTime dtDateFrom = id.DateFrom;
            TimeSpan tsDateFrom = new TimeSpan(0, 0, 0);
            id.DateFrom = dtDateFrom.Date + tsDateFrom;

            DateTime dtDateTo = id.DateTo;
            TimeSpan tsDateTo = new TimeSpan(0, 0, 0);
            id.DateTo = dtDateTo.Date + tsDateTo;

            List<VacantByCompany> vacantsByCompaniesTemp = db.VacantByCompanies.Include(r => r.Company).Include(r => r.VacantLevel).ToList();
            foreach (VacantByCompany c in vacantsByCompaniesTemp)
            {
                DateTime date = c.RequestDate;
                TimeSpan ts = new TimeSpan(0, 0, 0);
                date = date.Date + ts;
                c.RequestDate = date;
                vacantsByCompanies.Add(c);
            }

            if (id.DateFrom.Year > 1 && id.DateTo.Year == 1)
            {
                vacantsByCompanies = vacantsByCompanies.Where(r => r.RequestDate >= id.DateFrom).ToList();
            }
            else if (id.DateFrom.Year == 1 && id.DateTo.Year > 1)
            {
                vacantsByCompanies = vacantsByCompanies.Where(r => r.RequestDate <= id.DateTo).ToList();

            }
            else if (id.DateFrom.Year > 1 && id.DateTo.Year > 1)
            {
                vacantsByCompanies = vacantsByCompanies.Where(r => r.RequestDate >= id.DateFrom && r.RequestDate <= id.DateTo).ToList();

            }

            List<VacantFilter> vacantList = new List<VacantFilter>();
            foreach (var vacant in vacantsByCompanies) 
            {
                VacantFilter coverd = new VacantFilter();
                coverd.VacantByCompany = vacant;
                coverd.VacantCovered = db.VacantCovers.Include(r => r.Employee).Where(r => r.VacantByCompanyId == vacant.VacantByCompanyId).ToList();
                vacantList.Add(coverd);
            }


            return Ok(vacantList);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/ExportVacantReport")]
        public HttpResponseMessage ExportVacantReport(VacantReportFilter id)
        {
            VacantReportFilter filters = id;
            try
            {
                if (filters != null)
                {
                    string filename = id.ReportName + ".xls";
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string documentPath = path + "Reports\\" + filename;
                    Reports.Helpers.Vacants.GenerateReport(filename, filters);
                }
                return Request.CreateResponse<VacantReportFilter>(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte");
            }
        }
    }
}