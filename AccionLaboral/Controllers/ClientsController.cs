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
using System.Web.Mvc;

namespace AccionLaboral.Controllers
{
    public class ClientsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Clients
        public IQueryable<Client> GetClients()
        {
            return db.Clients.Include(r => r.AcademicEducations.Select(c => c.City))
                .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel))
                .Include(r => r.AcademicEducations.Select(c => c.Career))
                .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                .Include(r => r.KnownPrograms)
                .Include(r => r.Languages.Select(l => l.Language))
                .Include(r => r.Languages.Select(l => l.LanguageLevel))
                .Include(r => r.References.Select(c => c.City))
                .Include(r => r.References.Select(t => t.ReferenceType))
                .Include(r => r.WorkExperiences)
                .Include(r => r.WorkExperiences.Select(c => c.City));
        }

        // GET api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Include(r => r.AcademicEducations.Select(c => c.City))
                .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel))
                .Include(r => r.AcademicEducations.Select(c => c.Career))
                .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                .Include(r => r.KnownPrograms)
                .Include(r => r.Languages.Select(l=>l.Language))
                .Include(r => r.Languages.Select(l => l.LanguageLevel))
                .Include(r => r.References.Select(c=>c.City))
                .Include(r => r.References.Select(t => t.ReferenceType))
                .Include(r => r.WorkExperiences)
                .Include(r => r.WorkExperiences.Select(c => c.City))
                .First(r => r.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT api/Clients/5
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ClientId == id) > 0;
        }

        //public ActionResult Test()
        //{
        //    return View();
        //}
    }
}