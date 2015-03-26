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
using AccionLaboral.Reports.Helpers;
using Novacode;

namespace AccionLaboral.Controllers
{
    public class ContractTemplatesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/ContractTemplates
        public IQueryable<ContractTemplate> GetContractTemplates()
        {
            return db.ContractTemplates;
        }

        [HttpGet]
        [Route("api/ContractTemplatesActive")]
        public IQueryable<ContractTemplate> ContractTemplatesActive()
        {
            return db.ContractTemplates.Where(r => r.Active);
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


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/ExportContractReport")]
        public HttpResponseMessage ExportContractReport(ContractFilter id)
        {
            ContractFilter filters = id;
            try
            {
                if (filters != null)
                {
                    string filename = "Contrato_" + filters.ClientData.FirstName + "_" + filters.ClientData.LastName + ".docx";
                    id.FileName = "Contrato_" + filters.ClientData.FirstName + "_" + filters.ClientData.LastName;
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string documentPath = path + "Reports\\" + filename;
                    string templatePath = path + "Reports" + "\\" + "ContractTemplate.docx";
                    using (DocX doc = DocX.Load(templatePath))
                    {
                        ContractTemplate contract = filters.ContractTemplateData;
                        contract.Description = filters.ContractContent;
                        Contracts.CreateContract(doc, contract);
                        doc.SaveAs(documentPath);
                    }
                }
                return Request.CreateResponse<ContractFilter>(HttpStatusCode.OK, id);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte");
            }
        }
    }
}