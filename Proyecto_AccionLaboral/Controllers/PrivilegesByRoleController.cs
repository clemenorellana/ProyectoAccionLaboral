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
using Proyecto_AccionLaboral.Models;

namespace Proyecto_AccionLaboral.Controllers
{
    public class PrivilegesByRoleController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/PrivilegesByRole
        public IQueryable<PrivilegesByRole> GetPrivilegesByRoles()
        {
            return db.PrivilegesByRoles;
        }

        // GET api/PrivilegesByRole/5
        [ResponseType(typeof(PrivilegesByRole))]
        public IHttpActionResult GetPrivilegesByRole(int id)
        {
            PrivilegesByRole privilegesbyrole = db.PrivilegesByRoles.Find(id);
            if (privilegesbyrole == null)
            {
                return NotFound();
            }

            return Ok(privilegesbyrole);
        }

        // PUT api/PrivilegesByRole/5
        public IHttpActionResult PutPrivilegesByRole(int id, PrivilegesByRole privilegesbyrole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != privilegesbyrole.PrivilegesByRoleId)
            {
                return BadRequest();
            }

            db.Entry(privilegesbyrole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivilegesByRoleExists(id))
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

        // POST api/PrivilegesByRole
        [ResponseType(typeof(PrivilegesByRole))]
        public IHttpActionResult PostPrivilegesByRole(PrivilegesByRole privilegesbyrole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PrivilegesByRoles.Add(privilegesbyrole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = privilegesbyrole.PrivilegesByRoleId }, privilegesbyrole);
        }

        // DELETE api/PrivilegesByRole/5
        [ResponseType(typeof(PrivilegesByRole))]
        public IHttpActionResult DeletePrivilegesByRole(int id)
        {
            PrivilegesByRole privilegesbyrole = db.PrivilegesByRoles.Find(id);
            if (privilegesbyrole == null)
            {
                return NotFound();
            }

            db.PrivilegesByRoles.Remove(privilegesbyrole);
            db.SaveChanges();

            return Ok(privilegesbyrole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrivilegesByRoleExists(int id)
        {
            return db.PrivilegesByRoles.Count(e => e.PrivilegesByRoleId == id) > 0;
        }
    }
}