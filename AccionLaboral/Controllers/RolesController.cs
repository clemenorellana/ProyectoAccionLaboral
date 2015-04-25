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
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace AccionLaboral.Controllers
{
    public class RolesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public RolesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/Roles
        public async Task<IHttpActionResult> GetRoles()
        {
            var roles = await db.Roles.ToListAsync();
            return Ok(roles);
        }

        // GET api/Roles/5
        [ResponseType(typeof(IdentityRole))]
        public IHttpActionResult GetRole(string id)
        {
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT api/Roles/5
        public IHttpActionResult PutRole(string id, IdentityRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST api/Roles
        [ResponseType(typeof(Role))]
        public IHttpActionResult PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }

        // DELETE api/Roles/5
        [ResponseType(typeof(IdentityRole))]
        public IHttpActionResult DeleteRole(string id)
        {
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            db.Roles.Remove(role);
            db.SaveChanges();

            return Ok(role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(string id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}