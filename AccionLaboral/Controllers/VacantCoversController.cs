using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AccionLaboral.Models;
using System;
using System.Collections.Generic;


namespace AccionLaboral.Controllers
{
    [Authorize]
    public class VacantCoversController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public VacantCoversController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/VacantCovered
        [HttpGet]
        [Route("api/VacantCovered")]
        public IQueryable<VacantCovered> GetVacantCovered()
        {
            return db.VacantCovers.Include(r => r.Employee);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/VacantsCovered/{id}")]
        public List<VacantCovered> VacantsCovered(int id)
        {
            List<VacantCovered> vacants = null;
            
            try
            {
                vacants = db.VacantCovers.Include(r => r.Employee).Where(r => r.VacantByCompanyId == id).ToList();


            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return vacants;
        }

        // POST api/VacantCovered
        [HttpPost]
        [Route("api/VacantCovered")]
        [ResponseType(typeof(VacantCovered))]
        public IHttpActionResult PostVacantByCompany(VacantCovered vacantcovered)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int exite = db.VacantCovers.Where(r => r.VacantByCompanyId == vacantcovered.VacantByCompanyId && r.EmployeeId == vacantcovered.EmployeeId).Count();

                if (exite == 0)
                    db.VacantCovers.Add(vacantcovered);
                else
                    db.Entry(vacantcovered).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch (Exception e) {
                var error = e.Message;
            }
            return CreatedAtRoute("DefaultApi", new { id = vacantcovered.VacantCoveredId }, vacantcovered);
        }
    }
}
