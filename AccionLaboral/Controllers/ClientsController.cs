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
using AccionLaboral.Helpers.Lucene;
using System.IO;
using AccionLaboral.Helpers.Filters;
using System.Web.Http.Cors;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace AccionLaboral.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientsController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public ClientsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Database.CommandTimeout = 180;
        }

        // GET api/Clients/{currentPage}/{recordsPerPage}
        [Authorize]
        [HttpPost]
        [Route("api/Clients/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult GetClients(int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.Age.ToString().Contains(term.Age ?? "")
                 && r.Email.ToLower().Contains(string.IsNullOrEmpty(term.Email) ? "" : term.Email.ToLower())
                 && r.CompleteAddress.ToLower().Contains(string.IsNullOrEmpty(term.CompleteAddress) ? "" : term.CompleteAddress.ToLower())
                 && r.Cellphone.Contains(term.Cellphone ?? "")
                 && r.StateId.ToString().Contains(term.StateId ?? ""))
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.EnrollDate,
                    x.Age,
                    x.StateId,
                    x.Email,
                    x.State,
                    x.CompleteAddress,
                    x.Cellphone,
                    x.IdentityNumber,
                    x.Employee.Address,
                    x.RejectionDescription
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);
        }

        // POST api/ClientsByEmployee/{id}/{currentPage}/{recordsPerPage}
        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/clientsbyemployee/{id}/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult ClientsByEmployee(int id, int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.Age.ToString().Contains(term.Age ?? "")
                 && r.Email.ToLower().Contains(string.IsNullOrEmpty(term.Email) ? "" : term.Email.ToLower())
                 && r.CompleteAddress.ToLower().Contains(string.IsNullOrEmpty(term.CompleteAddress) ? "" : term.CompleteAddress.ToLower())
                 && r.Cellphone.Contains(term.Cellphone ?? "")
                 && r.StateId.ToString().Contains(term.StateId ?? "")
                 && r.EmployeeId == id)
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.EnrollDate,
                    x.Age,
                    x.StateId,
                    x.Email,
                    x.State,
                    x.CompleteAddress,
                    x.Cellphone,
                    x.IdentityNumber,
                    x.Employee.Address,
                    x.RejectionDescription
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);
        }

        // Get api/enrolledclientsbyemployee/{id}/{currentPage}/{recordsPerPage}
        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/enrolledclientsbyemployee/{id}/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult GetEnrolledClients(int id, int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.State.Alias=="PI"
                 && r.EmployeeId == id)
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.EnrollDate,
                    x.IdentityNumber,
                    x.StateId,
                    x.EmployeeId,
                    x.State
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);
        }

        // Get api/enrolledclients
        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/enrolledclients/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult GetEnrolledClients(int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.State.Alias == "PI")
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.EnrollDate,
                    x.IdentityNumber,
                    x.StateId,
                    x.EmployeeId,
                    x.State
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);


        }

        // Get api/trackingclients/{currentPage}/{recordsPerPage}
        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/trackingclients/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult GetTrackingClients(int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee)
                .Include(r => r.Trackings.Select(c => c.TrackingType)).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.Age.ToString().Contains(term.Age ?? "")
                 && r.Email.ToLower().Contains(string.IsNullOrEmpty(term.Email) ? "" : term.Email.ToLower())
                 && r.CompleteAddress.ToLower().Contains(string.IsNullOrEmpty(term.CompleteAddress) ? "" : term.CompleteAddress.ToLower())
                 && r.Cellphone.Contains(term.Cellphone ?? "")
                 && r.StateId.ToString().Contains(term.StateId ?? "")
                 && r.Trackings.ToList().First().TrackingTypeId.ToString().Contains(term.TrackingTypeId ?? ""))
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.Employee.EmployeeAlias,
                    x.EnrollDate,
                    x.Age,
                    Trackings = x.Trackings.Select(c => new { c.TrackingType }),
                    x.CompleteAddress,
                    x.Cellphone,
                    StateId = x.StateId,
                    x.EmployeeId,
                    x.State
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);
        }

        // Get api/trackingclients/{id}/{currentPage}/{recordsPerPage}
        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/trackingclientsbyemployee/{id}/{currentPage}/{recordsPerPage}")]
        public IHttpActionResult GetTrackingClients(int id, int currentPage, int recordsPerPage, ClientFilter term)
        {
            term = term ?? new ClientFilter();
            var begin = (currentPage - 1) * recordsPerPage;

            var results = db.Clients.Include(r => r.State).Include(r => r.Employee)
                .Include(r => r.Trackings.Select(c => c.TrackingType)).AsEnumerable().Where(r =>
                   (r.FirstName.ToLower().Contains(string.IsNullOrEmpty(term.FirstName) ? "" : term.FirstName.ToLower())
                 && r.LastName.ToLower().Contains(string.IsNullOrEmpty(term.LastName) ? "" : term.LastName.ToLower())
                 && r.IdentityNumber.ToLower().Contains(term.IdentityNumber ?? "")
                 && r.Age.ToString().Contains(term.Age ?? "")
                 && r.Email.ToLower().Contains(string.IsNullOrEmpty(term.Email) ? "" : term.Email.ToLower())
                 && r.CompleteAddress.ToLower().Contains(string.IsNullOrEmpty(term.CompleteAddress) ? "" : term.CompleteAddress.ToLower())
                 && r.Cellphone.Contains(term.Cellphone ?? "")
                 && r.StateId.ToString().Contains(term.StateId ?? "")
                 && ((r.Trackings.ToList().Count() > 0) ? r.Trackings.ToList()[0].TrackingTypeId.ToString().Contains(term.TrackingTypeId ?? "") : false)
                 && r.EmployeeId == id)
                 );

            var clients = new
            {
                count = results.Count(),
                data = results.Select(x => new
                {
                    x.ClientId,
                    x.FirstName,
                    x.LastName,
                    x.Employee.EmployeeAlias,
                    x.EnrollDate,
                    x.Age,
                    Trackings = x.Trackings.Select(c => new { c.TrackingType }),
                    x.CompleteAddress,
                    x.Cellphone,
                    StateId = x.StateId,
                    x.EmployeeId,
                    x.State
                })
                .OrderByDescending(r => r.EnrollDate).Skip(begin).Take(recordsPerPage)
                .ToList()
            };

            if (clients == null)
                return NotFound();

            return Ok(clients);
        }

        // GET api/Clients/5
        [Authorize]
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
                return NotFound();
            }

            return Ok(client);
        }

        // GET api/Clients/5
        [Authorize]
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
        [Authorize]
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

            try
            {
                client.State = null;
                var dbClients = db.Clients
                           .Include(x => x.AcademicEducations)
                           .Include(x => x.Languages)
                           .Include(x => x.KnownPrograms)
                           .Include(x => x.WorkExperiences)
                           .Include(x => x.References)
                           .Include(x => x.Trackings)
                           .Single(c => c.ClientId == client.ClientId);
                //client.EnrollDate = DateTime.Now;
                if (client.Photo!=null)
                client.Photo = ConvertImage(client.Photo, 96, 96);
                db.Entry(dbClients).CurrentValues.SetValues(client);
                if (client.AcademicEducations != null)
                {
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
                }

                ///Languajes
                if (client.Languages != null)
                {
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
                }

                //KnownPrograms
                if (client.KnownPrograms != null)
                {
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
                }

                //WorkExperiences

                if (client.WorkExperiences != null)
                {
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
                }

                //References
                if (client.References != null)
                {
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
                }

                //Trackings
                if (client.Trackings != null)
                {
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
                }

                db.SaveChanges();
                //GoLucene.AddUpdateLuceneIndex(client);
            }
            catch (Exception)
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

        // Put api/ChangeClientValues
        [Authorize]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/changeclientvalues/{id}")]
        public IHttpActionResult ChangeClientValues(int id, Client client)
        {
            client.State = null;
            try
            {
                var dbClients = db.Clients
                           .Include(x => x.AcademicEducations)
                           .Include(x => x.Languages)
                           .Include(x => x.KnownPrograms)
                           .Include(x => x.WorkExperiences)
                           .Include(x => x.References)
                           .Include(x => x.Trackings)
                           .Single(c => c.ClientId == client.ClientId);
                db.Entry(dbClients).CurrentValues.SetValues(client);
                db.SaveChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);

        }

        // Put api/enrollclient
        [Authorize]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/enrollclient/{id}")]
        public IHttpActionResult EnrollClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            try
            {
                client.State = null;
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

                //References
                if (client.References != null)
                {
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
                }


                db.SaveChanges();
                //GoLucene.AddUpdateLuceneIndex(client);
            }
            catch (Exception)
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

        // Put api/trackingclient
        [Authorize]
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/clienttracking/{id}")]
        public IHttpActionResult ClientTracking(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            try
            {
                client.State = null;
                var dbClients = db.Clients
                           .Include(x => x.AcademicEducations)
                           .Include(x => x.Languages)
                           .Include(x => x.KnownPrograms)
                           .Include(x => x.WorkExperiences)
                           .Include(x => x.References)
                           .Include(x => x.Trackings)
                           .Single(c => c.ClientId == client.ClientId);

                db.Entry(dbClients).CurrentValues.SetValues(client);

                //Trackings
                if (client.Trackings != null)
                {
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
                }


                db.SaveChanges();
                //GoLucene.AddUpdateLuceneIndex(client);
            }
            catch (Exception)
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
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (client.Photo != null)
                client.Photo = ConvertImage(client.Photo, 96, 96);
                client.EnrollDate = DateTime.Now;
                db.Clients.Add(client);

                db.SaveChanges();
                //await Task.Run(()=>
                //GoLucene.AddUpdateLuceneIndex(client));
            }
            catch (Exception)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE api/Clients/5
        [Authorize]
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Include(r=>r.Trackings).First(c=>c.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            //GoLucene.ClearLuceneIndexRecord(id);
            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/refreshclients")]
        public HttpResponseMessage RefreshClients()
        {
            List<Client> clients = null;
            try
            {
                clients = db.Clients
                    .Include(r => r.AcademicEducations.Select(c => c.City.Country))
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
                    .Include(r => r.Trackings.Select(c => c.TrackingType))
                    .ToList();
                GoLucene.ClearLuceneIndex();
                GoLucene.AddUpdateLuceneIndex(clients);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse<List<Client>>(HttpStatusCode.OK, clients);
        }

        // Get api/ExportClient
        [Authorize]
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

        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/exportclients")]
        public HttpResponseMessage ExportClients(ClientsFilter id)
        {
            try
            {
                ClientsFilter filters = id;
                DateTime DateFrom = (string.IsNullOrEmpty(id.DateFrom)) ? new DateTime(1, 1, 1) : Convert.ToDateTime(id.DateFrom);
                DateTime DateTo = (string.IsNullOrEmpty(id.DateTo)) ? new DateTime(9999, 1, 1) : Convert.ToDateTime(id.DateTo);

                id.Clients = db.Clients
                            .Include(r => r.State)
                            .Include(r => r.Employee)
                            .Where(r => r.EnrollDate >= DateFrom && r.EnrollDate <= DateTo).ToList();

                if (DateFrom.Year != 1)
                    id.DateFrom = DateFrom.ToString("dd/MM/yyyy");
                if (DateTo.Year != 9999)
                    id.DateTo = DateTo.ToString("dd/MM/yyyy");

                if (filters != null)
                {
                    string filename = filters.Title.Replace(".", " ") + ".xls";
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

        [Authorize]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/exportclientstracking")]
        public HttpResponseMessage ExportClientsTracking(ClientsFilter id)
        {
            try
            {
                ClientsFilter filters = id;
                DateTime DateFrom = (string.IsNullOrEmpty(id.DateFrom)) ? new DateTime(1, 1, 1) : Convert.ToDateTime(id.DateFrom);
                DateTime DateTo = (string.IsNullOrEmpty(id.DateTo)) ? new DateTime(9999, 1, 1) : Convert.ToDateTime(id.DateTo);

                id.Clients = db.Clients.
                    Include(r => r.Trackings.Select(c => c.TrackingType)).
                    Include(r => r.State).
                    Include(r=>r.Employee).
                    Where(r => r.EnrollDate >= DateFrom && r.EnrollDate <= DateTo).ToList();

                if (DateFrom.Year != 1)
                    id.DateFrom = DateFrom.ToString("dd/MM/yyyy");
                if (DateTo.Year != 9999)
                    id.DateTo = DateTo.ToString("dd/MM/yyyy");

                if (filters != null)
                {
                    string filename = filters.Title.Replace(".", " ") + ".xls";
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
        [Authorize]
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

        private byte[] ConvertImage(byte[] myBytes, int newWidth, int newHeight)
        {
            var jpegQuality = 50;
            System.Drawing.Image image;
            using (var inputStream = new MemoryStream(myBytes))
            {
                image = System.Drawing.Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);
                
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    myBytes = outputStream.ToArray();
                }
            }
            return myBytes;
        }

        #region findclientbyidentitynumber

        public class FindClient
        {
            public int EmployeeId { get; set; }
            public string EmployeeRolAlias { get; set; }
            public string ClientIdentityNumber { get; set; }
        }
        
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/FindClientByIdentityNumber/")]
        public IHttpActionResult FindClientByIdentityNumber(FindClient id)
        {
            try
            {
                if (id.EmployeeRolAlias == "ASREC")
                {
                    var client = db.Clients
                            .Include(r => r.References)
                            .Select(x => new Client
                            {
                                ClientId = x.ClientId,
                                EmployeeId = x.EmployeeId,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Age = x.Age,
                                BBPin = x.BBPin,
                                Birthday = x.Birthday,
                                Cellphone = x.Cellphone,
                                EnrollDate = x.EnrollDate,
                                HaveCar = x.HaveCar,
                                HaveLicense = x.HaveLicense,
                                HaveMotorcycle = x.HaveMotorcycle,
                                IdentityNumber = x.IdentityNumber,
                                IsStudying = x.IsStudying,
                                LicenseType = x.LicenseType,
                                Occupation = x.Occupation,
                                QtyClasses = x.QtyClasses,
                                WageAspiration = x.WageAspiration,
                                Twitter = x.Twitter,
                                CompaniesWithPreviouslyRequested = x.CompaniesWithPreviouslyRequested,
                                CompleteAddress = x.CompleteAddress,
                                CorrelativeCode = x.CorrelativeCode,
                                DesiredEmployment = x.DesiredEmployment,
                                Email = x.Email,
                                EnglishPercentage = x.EnglishPercentage,
                                FacebookEmail = x.FacebookEmail,
                                Neighborhood = x.Neighborhood,
                                CurrentStudies = x.CurrentStudies,
                                References = x.References
                            }
                            )
                            .Where(r => r.EmployeeId == id.EmployeeId && r.IdentityNumber == id.ClientIdentityNumber)
                            .First()
                            ;

                    return Ok(client);
                }
                else
                {
                    var client = db.Clients
                           .Select(x => new
                           {
                               x.ClientId,
                               x.EmployeeId,
                               x.FirstName,
                               x.LastName,
                               x.Age,
                               x.BBPin,
                               x.Birthday,
                               x.Cellphone,
                               x.EnrollDate,
                               x.HaveCar,
                               x.HaveLicense,
                               x.HaveMotorcycle,
                               x.IdentityNumber,
                               x.IsStudying,
                               x.LicenseType,
                               x.Occupation,
                               x.QtyClasses,
                               x.WageAspiration,
                               x.Twitter,
                               x.CompaniesWithPreviouslyRequested,
                               x.CompleteAddress,
                               x.CorrelativeCode,
                               x.DesiredEmployment,
                               x.Email,
                               x.EnglishPercentage,
                               x.FacebookEmail,
                               x.Neighborhood,
                               x.CurrentStudies,
                               x.References
                           }
                           )
                           .Where(r => r.IdentityNumber == id.ClientIdentityNumber)
                           .First();

                    return Ok(client);
                }
            }
            catch (Exception e)
            {
                var x = e.Message;
            }

            NotFound();
            Client c = null;

            return Ok(c);
        }
        

        #endregion
    }
}