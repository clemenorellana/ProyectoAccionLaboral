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
    [Authorize]
    public class InterviewTypesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public InterviewTypesController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/InterviewTypes
        public IQueryable<InterviewType> GetInterviewTypes()
        {
            return db.InterviewTypes;
        }

        // GET api/InterviewTypes/5
        [ResponseType(typeof(InterviewType))]
        public IHttpActionResult GetInterviewType(int id)
        {
            InterviewType interviewtype = db.InterviewTypes.Find(id);
            if (interviewtype == null)
            {
                return NotFound();
            }

            return Ok(interviewtype);
        }

        // PUT api/InterviewTypes/5
        public IHttpActionResult PutInterviewType(int id, InterviewType interviewtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interviewtype.InterviewTypeId)
            {
                return BadRequest();
            }

            db.Entry(interviewtype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewTypeExists(id))
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

        // POST api/InterviewTypes
        [ResponseType(typeof(InterviewType))]
        public IHttpActionResult PostInterviewType(InterviewType interviewtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InterviewTypes.Add(interviewtype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = interviewtype.InterviewTypeId }, interviewtype);
        }

        // DELETE api/InterviewTypes/5
        [ResponseType(typeof(InterviewType))]
        public IHttpActionResult DeleteInterviewType(int id)
        {
            InterviewType interviewtype = db.InterviewTypes.Find(id);
            if (interviewtype == null)
            {
                return NotFound();
            }

            db.InterviewTypes.Remove(interviewtype);
            db.SaveChanges();

            return Ok(interviewtype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InterviewTypeExists(int id)
        {
            return db.InterviewTypes.Count(e => e.InterviewTypeId == id) > 0;
        }
    }
}
