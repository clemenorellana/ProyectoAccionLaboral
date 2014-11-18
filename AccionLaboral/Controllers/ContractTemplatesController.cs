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
    public class ContractTemplatesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/ContractTemplates
        public IQueryable<ContractTemplate> GetContractTemplates()
        {
            return db.ContractTemplates;
        }

        // GET api/ContractTemplates/5
        [ResponseType(typeof(ContractTemplate))]
        public IHttpActionResult GetContractTemplate(int id)
        {
            ContractTemplate contracttemplate = db.ContractTemplates.Find(id);
            if (contracttemplate == null)
            {
                return NotFound();
            }

            return Ok(contracttemplate);
        }

        // PUT api/ContractTemplates/5
        public IHttpActionResult PutContractTemplate(int id, ContractTemplate contracttemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contracttemplate.ContractTemplateId)
            {
                return BadRequest();
            }

            db.Entry(contracttemplate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractTemplateExists(id))
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

        // POST api/ContractTemplates
        [ResponseType(typeof(ContractTemplate))]
        public IHttpActionResult PostContractTemplate(ContractTemplate contracttemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContractTemplates.Add(contracttemplate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contracttemplate.ContractTemplateId }, contracttemplate);
        }

        // DELETE api/ContractTemplates/5
        [ResponseType(typeof(ContractTemplate))]
        public IHttpActionResult DeleteContractTemplate(int id)
        {
            ContractTemplate contracttemplate = db.ContractTemplates.Find(id);
            if (contracttemplate == null)
            {
                return NotFound();
            }

            db.ContractTemplates.Remove(contracttemplate);
            db.SaveChanges();

            return Ok(contracttemplate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractTemplateExists(int id)
        {
            return db.ContractTemplates.Count(e => e.ContractTemplateId == id) > 0;
        }
    }
}