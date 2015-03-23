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
using AccionLaboral.Helpers.Lucene;
using System.IO;
using AccionLaboral.Helpers.Filters;

namespace AccionLaboral.Controllers
{
    [System.Web.Http.RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public ClientsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/Clients
        public List<Client> GetClients()
        {
            
            List<Client> clients = null;
            try
            {
                clients = db.Clients
                    //.Include(r => r.AcademicEducations.Select(c => c.City.Country))
                    //.Include(r => r.Employee)
                    .Include(r => r.State)
                    /*.Include(r => r.AcademicEducations.Select(l => l.AcademicLevel.Careers))
                    .Include(r => r.AcademicEducations.Select(c => c.Career))
                    .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                    .Include(r => r.KnownPrograms)
                    .Include(r => r.Languages.Select(l => l.Language))
                    .Include(r => r.Languages.Select(l => l.LanguageLevel))
                    .Include(r => r.References.Select(c => c.City.Country))
                    .Include(r => r.References.Select(t => t.ReferenceType))
                    .Include(r => r.WorkExperiences)
                    .Include(r => r.WorkExperiences.Select(c => c.City.Country))*/
                    .Include(r => r.Trackings.Select(c => c.TrackingType))
                    .AsEnumerable().Select(x => new Client
                    {
                        ClientId = x.ClientId,
                        CorrelativeCode = x.CorrelativeCode,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Age = x.Age,
                        Email = x.Email,
                        CompleteAddress = x.CompleteAddress,
                        Cellphone = x.Cellphone,
                        EnrollDate = x.EnrollDate,
                        //AcademicEducations = x.AcademicEducations,
                        Employee = new Employee { Age = x.Age },
                        State = x.State,
                        Trackings = x.Trackings

                    }).
                ToList();
                //GoLucene.ClearLuceneIndex();
                //GoLucene.AddUpdateLuceneIndex(clients);
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return clients;
        }

        // Get api/ClientsByEmployee
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/clientsbyemployee/{id}")]
        public List<Client> ClientsByEmployee(int id)
        {
            List<Client> clients = null;
            var dbQuery = clients;
            try
            {
                clients = db.Clients
                    .Include(r => r.AcademicEducations.Select(c => c.City.Country))
                    .Include(r => r.Employee)
                    .Include(r => r.State)
                    /*       .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel.Careers))
                           .Include(r => r.AcademicEducations.Select(c => c.Career))
                           .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                           .Include(r => r.KnownPrograms)
                           .Include(r => r.Languages.Select(l => l.Language))
                           .Include(r => r.Languages.Select(l => l.LanguageLevel))
                           .Include(r => r.References.Select(c => c.City.Country))
                           .Include(r => r.References.Select(t => t.ReferenceType))
                           .Include(r => r.WorkExperiences)
                           .Include(r => r.WorkExperiences.Select(c => c.City.Country))*/
                    .Include(r => r.Trackings.Select(c => c.TrackingType))
                    .Where(r => r.EmployeeId == id)
                    .ToList();
                dbQuery = (from c in db.Clients
                              join s in db.States
                                on c.StateId equals s.StateId
                             where c.EmployeeId == id
                           select new Client { FirstName = c.FirstName,
                                               LastName = c.LastName, 
                                               Age = c.Age, 
                                               Email = c.Email,
                                               Cellphone = c.Cellphone,
                                               CompleteAddress = c.CompleteAddress,
                                               State = c.State
                           }).ToList();
                
                            
            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return dbQuery;
        }


        // GET api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = null;
            try
            {
                client = db.Clients.Include(r => r.AcademicEducations.Select(c => c.City.Country))
                    .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel))
                    .Include(r => r.AcademicEducations.Select(c => c.Career))
                    .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                    .Include(r => r.KnownPrograms)
                    .Include(r => r.Languages.Select(l => l.Language))
                    .Include(r => r.Languages.Select(l => l.LanguageLevel))
                    .Include(r => r.References.Select(c => c.City))
                    .Include(r => r.References.Select(t => t.ReferenceType))
                    .Include(r => r.WorkExperiences)
                    .Include(r => r.WorkExperiences.Select(c => c.City))
                    .Include(r => r.State)
                    .Include(r => r.Trackings.Select(c => c.TrackingDetails.Select(d => d.ShipmentType)))
                    .Include(r => r.Trackings.Select(c => c.State))
                    .Include(r => r.Trackings.Select(c => c.TrackingType))
                    .First(r => r.ClientId == id);

                if (client == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                var x = e.Message;
            }

            return Ok(client);
        }

        // GET api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult Tracking(int id)
        {
            Client client = db.Clients.Include(r => r.Trackings.Select(c => c.TrackingDetails))
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
            var dbClients = db.Clients
                       .Include(x => x.AcademicEducations)
                       .Include(x => x.Languages)
                       .Include(x => x.KnownPrograms)
                       .Include(x => x.WorkExperiences)
                       .Include(x => x.References)
                       .Include(x => x.Trackings)
                       .Single(c => c.ClientId == client.ClientId);
            client.EnrollDate = DateTime.Now;
            db.Entry(dbClients).CurrentValues.SetValues(client);

            foreach (var dbAcademicEducation in dbClients.AcademicEducations.ToList())
                if (!client.AcademicEducations.Any(s => s.AcademicEducationId == dbAcademicEducation.AcademicEducationId))
                    db.AcademicEducations.Remove(dbAcademicEducation);

            foreach (var newAcademicEducation in client.AcademicEducations)
            {
                newAcademicEducation.ClientId = client.ClientId;
                if (newAcademicEducation.AcademicEducationId != 0)
                {
                    var dbAcademicEducation = dbClients.AcademicEducations.SingleOrDefault(s => s.AcademicEducationId == newAcademicEducation.AcademicEducationId);
                    db.Entry(dbAcademicEducation).CurrentValues.SetValues(newAcademicEducation);
                }   
                else
                {
                    newAcademicEducation.City = null;
                    newAcademicEducation.Career = null;
                    newAcademicEducation.AcademicLevel = null;
                    newAcademicEducation.EducationType = null;
                    dbClients.AcademicEducations.Add(newAcademicEducation);
                }
            }

            ///Languajes
            foreach (var dbLanguage in dbClients.Languages.ToList())
                if (!client.Languages.Any(s => s.KnownLanguageId == dbLanguage.KnownLanguageId))
                    db.KnownLanguages.Remove(dbLanguage);

            foreach (var newLanguage in client.Languages)
            {
                
                newLanguage.ClientId = client.ClientId;
                newLanguage.LanguageLevel = null;
                newLanguage.LanguageLevel = null;
                if (newLanguage.KnownLanguageId != 0)
                {
                    var dbLanguage = dbClients.Languages.SingleOrDefault(s => s.KnownLanguageId == newLanguage.KnownLanguageId);
                    db.Entry(dbLanguage).CurrentValues.SetValues(newLanguage);
                }
                else
                {
                    dbClients.Languages.Add(newLanguage);
                }
            }

            //KnownPrograms
            foreach (var dbProgram in dbClients.KnownPrograms.ToList())
                if (!client.KnownPrograms.Any(s => s.KnownProgramId == dbProgram.KnownProgramId))
                    db.KnownPrograms.Remove(dbProgram);

            foreach (var newProgram in client.KnownPrograms)
            {
                newProgram.ClientId = client.ClientId;
                if (newProgram.KnownProgramId != 0)
                {
                    var dbProgram = dbClients.KnownPrograms.SingleOrDefault(s => s.KnownProgramId == newProgram.KnownProgramId);
                    db.Entry(dbProgram).CurrentValues.SetValues(newProgram);
                }
                else
                {
                    dbClients.KnownPrograms.Add(newProgram);
                }
            }

            //WorkExperiences
            foreach (var dbWorkExperience in dbClients.WorkExperiences.ToList())
                if (!client.WorkExperiences.Any(s => s.WorkExperienceId == dbWorkExperience.WorkExperienceId))
                    db.WorkExperiences.Remove(dbWorkExperience);

            foreach (var newWorkExperience in client.WorkExperiences)
            {
                newWorkExperience.ClientId = client.ClientId;
                if (newWorkExperience.WorkExperienceId != 0)
                {
                    var dbWorkExperience = dbClients.WorkExperiences.SingleOrDefault(s => s.WorkExperienceId == newWorkExperience.WorkExperienceId);
                    db.Entry(dbWorkExperience).CurrentValues.SetValues(newWorkExperience);
                }
                else
                {
                    newWorkExperience.City = null;
                    dbClients.WorkExperiences.Add(newWorkExperience);
                }
            }
            //References
            foreach (var dbReference in dbClients.References.ToList())
                if (!client.References.Any(s => s.ReferenceId == dbReference.ReferenceId))
                    db.References.Remove(dbReference);

            foreach (var newReference in client.References)
            {
                newReference.City = null;
                newReference.ReferenceType = null;
                newReference.ClientId = client.ClientId;
                if (newReference.ReferenceId != 0)
                {
                    var dbReference = dbClients.References.SingleOrDefault(s => s.ReferenceId == newReference.ReferenceId);
                    db.Entry(dbReference).CurrentValues.SetValues(newReference);   
                }
                else
                {
                    dbClients.References.Add(newReference);
                }
                    
            }
            
            //Trackings
            foreach (var dbSubFoo in dbClients.Trackings.ToList())
                if (!client.Trackings.Any(s => s.TrackingId == dbSubFoo.TrackingId))
                    db.Trackings.Remove(dbSubFoo);

            foreach (var newSubFoo in client.Trackings)
            {
                var dbSubFoo = dbClients.Trackings.SingleOrDefault(s => s.TrackingId == newSubFoo.TrackingId);
                if (dbSubFoo != null)
                    db.Entry(dbSubFoo).CurrentValues.SetValues(newSubFoo);
                else
                    dbClients.Trackings.Add(newSubFoo);
            }
            try
            {
                GoLucene.AddUpdateLuceneIndex(client);
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                client.EnrollDate = DateTime.Now;
                db.Clients.Add(client);
                
                db.SaveChanges();
                GoLucene.AddUpdateLuceneIndex(client);
            }
            catch( Exception e){
                var x = e.Message;
            }
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
            GoLucene.ClearLuceneIndexRecord(id);
            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        // Get api/ExportClient
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/exportclient/{id}")]
        public string ExportClient(int id)
        {
            string path = "";
            try
            {
                
                Client client = db.Clients.
                    Include(r => r.AcademicEducations.Select(c => c.City.Country))
                .Include(r => r.Employee)
                .Include(r => r.State)
                .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel.Careers))
                .Include(r => r.AcademicEducations.Select(c => c.Career))
                .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                .Include(r => r.KnownPrograms)
                .Include(r => r.Languages.Select(l => l.Language))
                .Include(r => r.Languages.Select(l => l.LanguageLevel))
                .Include(r => r.References.Select(c => c.City.Country))
                .Include(r => r.References.Select(t => t.ReferenceType))
                .Include(r => r.WorkExperiences)
                .Include(r => r.WorkExperiences.Select(c => c.City.Country))
                .Include(r => r.City.Country).First(r => r.ClientId == id);

                if (client != null)
                {
                    
                    //AccionLaboral.Reports.Helpers.CV.CreateWordDocument(client,ref path);
                    //return true;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return path;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/exportclients")]
        //[System.Web.Http.Route("api/exportclients/{id}")]
        public HttpResponseMessage ExportClients(ClientsFilter id)
        {
            ClientsFilter filters = id;
            id.DateFrom = (string.IsNullOrEmpty(id.DateFrom)) ? "" : Convert.ToDateTime(id.DateFrom).ToString("dd/MM/yyyy");
            id.DateTo = (string.IsNullOrEmpty(id.DateTo)) ? "" : Convert.ToDateTime(id.DateTo).ToString("dd/MM/yyyy");
            try
            {
                if (filters != null)
                {
                    string filename = "Clientes.xls";
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string documentPath = path + "Reports\\" + filename;
                    Reports.Helpers.Clients.GenerateReport(filename, filters);
                }
            return Request.CreateResponse<ClientsFilter>(HttpStatusCode.OK, id);
            }
            catch (Exception)
            {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte");  
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/exportclientstracking")]
        //[System.Web.Http.Route("api/exportclientstracking/{id}")]
        public HttpResponseMessage ExportClientsTracking(ClientsFilter id)
        {
            ClientsFilter filters = id;

            id.DateFrom = (string.IsNullOrEmpty(id.DateFrom)) ? "" : Convert.ToDateTime(id.DateFrom).ToString("dd/MM/yyyy");
            id.DateTo = (string.IsNullOrEmpty(id.DateTo)) ? "" : Convert.ToDateTime(id.DateTo).ToString("dd/MM/yyyy");
            try
            {
                
            if (filters != null)
            {
                string filename = "SeguimientoClientes.xls";
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string documentPath = path + "Reports\\" + filename;
                Reports.Helpers.Clients.GenerateClientsTracking(filename, filters);
            }

            return Request.CreateResponse<ClientsFilter>(HttpStatusCode.OK, id);
            }
            catch (Exception)
            {
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte");  
            }
        }

        // Get api/ExportClient
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/SearchClients/{searchTerm}/{searchField}/{limit}")]
        public List<Client> SearchClients(string searchTerm, string searchField, int? limit)
        {
            searchTerm = (searchTerm == "null") ? null : searchTerm;
            searchField = (searchField == "null") ? null : searchField;
            string[] fields = (searchField != null) ? searchField.Split(',') : null;
            // create default Lucene search index directory
            if (!Directory.Exists(GoLucene._luceneDir))
            {
                Directory.CreateDirectory(GoLucene._luceneDir);
                GoLucene.AddUpdateLuceneIndex(db.Clients
                                                        .Include(r => r.AcademicEducations.Select(c => c.City.Country))
                                                        //.Include(r => r.Employee)
                                                        .Include(r => r.State)
                                                        .Include(r => r.AcademicEducations.Select(l => l.AcademicLevel.Careers))
                                                        .Include(r => r.AcademicEducations.Select(c => c.Career))
                                                        .Include(r => r.AcademicEducations.Select(t => t.EducationType))
                                                        .Include(r => r.KnownPrograms)
                                                        .Include(r => r.Languages.Select(l => l.Language))
                                                        .Include(r => r.Languages.Select(l => l.LanguageLevel))
                                                        .Include(r => r.References.Select(c => c.City.Country))
                                                        //.Include(r => r.References.Select(t => t.ReferenceType))
                                                        .Include(r => r.WorkExperiences)
                                                        .Include(r => r.WorkExperiences.Select(c => c.City.Country))
                                                        .Include(r => r.Trackings.Select(c => c.TrackingType)).ToList());
            }

            // perform Lucene search
            List<Client> _searchResults;
                _searchResults = (string.IsNullOrEmpty(searchField)
                                   ? GoLucene.Search(searchTerm)
                                   : GoLucene.Search(searchTerm, fields)).ToList();
            if (string.IsNullOrEmpty(searchTerm) && !_searchResults.Any())
                _searchResults = GoLucene.GetAllIndexRecords().ToList();

            // limit display number of database records
            var limitDb = limit == null ? 10 : Convert.ToInt32(limit);
            List<Client> allSampleData;
            if (limitDb > 0)
            {
                allSampleData = db.Clients.ToList().Take(limitDb).ToList();
                //ViewBag.Limit = db.Clients.ToList().Count - limitDb;
            }
            else allSampleData = db.Clients.ToList();
            foreach (Client client in _searchResults)
            {
                client.State = (client.StateId != 0) ? db.States.Find(client.StateId) : null;
            }
            return _searchResults.AsQueryable().ToList();
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

        private void AddAcademicEducations(IEnumerable<AcademicEducation> academicEducations, Client client)
        {
            foreach (AcademicEducation academicEducation in academicEducations)
            {
                if (!AcademicEducationExists(academicEducation.AcademicEducationId))
                {
                    db.AcademicEducations.Add(academicEducation);
                    client.AcademicEducations.Remove(academicEducation);
                }
            }
        }
        private bool AcademicEducationExists(int id)
        {
            return db.AcademicEducations.Count(e => e.AcademicEducationId == id) > 0;
        }

        private void AddKnownLanguage(IEnumerable<KnownLanguage> knownLanguages, Client client)
        {
            foreach (KnownLanguage knownLanguage in knownLanguages)
            {
                if (!KnownLanguageExists(knownLanguage.KnownLanguageId))
                {
                    db.KnownLanguages.Add(knownLanguage);
                    client.Languages.Remove(knownLanguage);
                }
            }
        }
        private bool KnownLanguageExists(int id)
        {
            return db.KnownLanguages.Count(e => e.KnownLanguageId == id) > 0;
        }

        private void AddKnownPrograms(IEnumerable<KnownProgram> knownPrograms, Client client)
        {
            foreach (KnownProgram knownProgram in knownPrograms)
            {
                if (!KnownProgramExists(knownProgram.KnownProgramId))
                {
                    db.KnownPrograms.Add(knownProgram);
                    client.KnownPrograms.Remove(knownProgram);
                }
            }
        }
        private bool KnownProgramExists(int id)
        {
            return db.KnownPrograms.Count(e => e.KnownProgramId == id) > 0;
        }

        private void AddWorkExperience(IEnumerable<WorkExperience> workExperiences, Client client)
        {
            foreach (WorkExperience workExperience in workExperiences)
            {
                if (!WorkExperienceExists(workExperience.WorkExperienceId))
                {
                    db.WorkExperiences.Add(workExperience);
                    client.WorkExperiences.Remove(workExperience);
                }
            }
        }
        private bool WorkExperienceExists(int id)
        {
            return db.WorkExperiences.Count(e => e.WorkExperienceId == id) > 0;
        }

        private void AddReferences(IEnumerable<Reference> references, Client client)
        {
            foreach (Reference reference in references)
            {
                if (!ReferenceExists(reference.ReferenceId))
                {
                    db.References.Add(reference);
                    client.References.Remove(reference);
                }
            }
        }
        private bool ReferenceExists(int id)
        {
            return db.References.Count(e => e.ReferenceId == id) > 0;
        }

        private void AddTranckings(IEnumerable<Tracking> trackings, Client client)
        {
            foreach (Tracking tracking in trackings)
            {
                if (!TranckingExists(tracking.TrackingId))
                {
                    db.Trackings.Add(tracking);
                    client.Trackings.Remove(tracking);
                }
            }
        }
        private bool TranckingExists(int id)
        {
            return db.Trackings.Count(e => e.TrackingId == id) > 0;
        }
    }
}