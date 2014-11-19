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
    public class PrivilegesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Privileges
        public IQueryable<Privilege> GetPrivileges()
        {
            return db.Privileges;
        }

        // GET api/Privileges/5
        [ResponseType(typeof(Privilege))]
        public IHttpActionResult GetPrivilege(int id)
        {
            Privilege privilege = db.Privileges.Find(id);
            if (privilege == null)
            {
                return NotFound();
            }

            return Ok(privilege);
        }

        // PUT api/Privileges/5
        public IHttpActionResult PutPrivilege(int id, Privilege privilege)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != privilege.PrivilegeId)
            {
                return BadRequest();
            }

            db.Entry(privilege).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivilegeExists(id))
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

        // POST api/Privileges
        [ResponseType(typeof(Privilege))]
        public IHttpActionResult PostPrivilege(Privilege privilege)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Privileges.Add(privilege);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = privilege.PrivilegeId }, privilege);
        }

        // DELETE api/Privileges/5
        [ResponseType(typeof(Privilege))]
        public IHttpActionResult DeletePrivilege(int id)
        {
            Privilege privilege = db.Privileges.Find(id);
            if (privilege == null)
            {
                return NotFound();
            }

            db.Privileges.Remove(privilege);
            db.SaveChanges();

            return Ok(privilege);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrivilegeExists(int id)
        {
            return db.Privileges.Count(e => e.PrivilegeId == id) > 0;
        }
    }
}