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
using Microsoft.Ajax.Utilities;
using System.Web.Http.Cors;

namespace AccionLaboral.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AcademicEducationsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/academicEducations
        public AcademicEducationsController()
        {
            db.Database.CommandTimeout = 180;
        }
        
        [HttpGet]
        public IQueryable<AcademicEducation> GetAcademicEducations()
        {
            return db.AcademicEducations.Include(r => r.City)
                .Include(r => r.AcademicLevel).Include(r => r.EducationType);
        }

        // GET api/AcademicEducations/5
        [ResponseType(typeof(AcademicEducation))]
        //[Route("api/client/{clientId}/academicEducations/{academicEducationId}")]
        [HttpGet]
        public IHttpActionResult GetAcademicEducation(int academicEducationId)
        {

            AcademicEducation academiceducation = db.AcademicEducations.Include(r => r.AcademicLevel)
                .Include(r => r.Career)
                .Include(r => r.City)
                .Include(r => r.Client)
                .Include(r => r.EducationType)
                .First(r => r.AcademicEducationId == academicEducationId);
            
            if (academiceducation == null)
            {
                return NotFound();
            }

            return Ok(academiceducation);
        }

        // PUT api/AcademicEducations/5
        [Route("api/client/{clientId}/academicEducations/{academicEducationId}")]
        [HttpPut]
        public IHttpActionResult PutAcademicEducation(int clientId, int academicEducationId, AcademicEducation academiceducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = db.Clients.Find(clientId);
            if (clientId == null)
            {
                return BadRequest();
            }

            if (academicEducationId != academiceducation.AcademicEducationId)
            {
                return BadRequest();
            }

            academiceducation.Client = client;
            academiceducation.ClientId = client.ClientId;
            db.Entry(academiceducation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicEducationExists(academicEducationId))
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

        // POST api/AcademicEducations
        [ResponseType(typeof(AcademicEducation))]
        [Route("api/client/{clientId}/academicEducations")]
        [HttpPost]
        public IHttpActionResult PostAcademicEducation(int clientId, AcademicEducation academiceducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = db.Clients.Find(clientId);
            if (client == null)
                return BadRequest();

            academiceducation.Client = client;
            academiceducation.ClientId = clientId;

            db.AcademicEducations.Add(academiceducation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = academiceducation.AcademicEducationId }, academiceducation);
        }

        // DELETE api/AcademicEducations/5
        [ResponseType(typeof(AcademicEducation))]
        [Route("api/client/{clientId}/academicEducations/{academicEducationId}")]
        [HttpDelete]
        public IHttpActionResult DeleteAcademicEducation(int clientId, int academicEducationId)
        {
            var client = db.Clients.Find(clientId);
            if (client == null)
                return NotFound();

            AcademicEducation academiceducation = db.AcademicEducations.Find(academicEducationId);
            if (academiceducation == null)
            {
                return NotFound();
            }

            db.AcademicEducations.Remove(academiceducation);
            db.SaveChanges();

            return Ok(academiceducation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcademicEducationExists(int id)
        {
            return db.AcademicEducations.Count(e => e.AcademicEducationId == id) > 0;
        }
    }
}