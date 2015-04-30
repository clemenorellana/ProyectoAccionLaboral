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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountriesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public CountriesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/Countries
        [Route("api/Countries")]
        [HttpGet]
        public IQueryable<Country> GetCountries()
        {
            return db.Countries.Include(r=>r.Cities);
        }

        // GET api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT api/Countries/5
        [Authorize]
        public IHttpActionResult PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.CountryId)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST api/Countries
        [Route("api/Countries")]
        [HttpPost]
        [ResponseType(typeof(Country))]
        [Authorize]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "countries", id = country.CountryId }, country);
        }

        // DELETE api/Countries/5
        [ResponseType(typeof(Country))]
        [Authorize]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.CountryId == id) > 0;
        }
    }
}