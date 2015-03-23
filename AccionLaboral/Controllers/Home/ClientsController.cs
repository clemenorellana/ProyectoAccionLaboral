using AccionLaboral.Helpers.Filters;
using AccionLaboral.Models;
using AccionLaboral.Reports.Helpers;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class ClientsController : Controller
    {
        //
        // GET: /Clients/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/Clients/Index.html", "text/html");
        }

        public ActionResult ClientsReport()
        {
            return new FilePathResult("~/Views/Reports/ClientsReport.html", "text/html");
        }

        public ActionResult ClientsTrackingReport()
        {
            return new FilePathResult("~/Views/Reports/ClientsTrackingReport.html", "text/html");
        }

        //
        // GET: /Clients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Clients/Create
        public ActionResult Create()
        {
            return new FilePathResult("~/Views/Clients/NewClient.html", "text/html");
        }

        //
        // POST: /Clients/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Clients/Edit/
        public ActionResult Edit()
        {
            return new FilePathResult("~/Views/Clients/EditClient.html", "text/html");
        }

        //
        // POST: /Clients/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Clients/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Clients/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: /Clients/Enroll/
        public ActionResult Enroll()
        {
            return new FilePathResult("~/Views/Clients/EnrollClient.html", "text/html");
        }

        // GET: /Clients/Tracking/
        public ActionResult Tracking()
        {
            return new FilePathResult("~/Views/Clients/ClientTracking.html", "text/html");
        }

        // GET: /Clients/ClientTracking/
        public ActionResult ClientTracking()
        {
            return new FilePathResult("~/Views/Clients/TrackingDetail.html", "text/html");
        }

        // GET: /Clients/SearchClient/
        public ActionResult SearchClients()
        {
            return new FilePathResult("~/Views/Clients/SearchClients.html", "text/html");
        }

        // GET: /Clients/ClientProfile/
        public ActionResult ClientProfile()
        {
            return new FilePathResult("~/Views/Clients/Profile.html", "text/html");
        }

        public ActionResult ExportClient(int id)
        {
            AccionLaboralContext db = new AccionLaboralContext();
            Client client = db.Clients.Include("AcademicEducations.City.Country")
                                      .Include("AcademicEducations.EducationType")
                                      .Include("KnownPrograms")
                                      .Include("Languages.Language")
                                      .Include("Languages.LanguageLevel")
                                      .Include("References.ReferenceType")
                                      .Include("References.City.Country")
                                      .Include("WorkExperiences.City.Country").First(r => r.ClientId == id);

            string filename = "CV_" + client.FirstName + client.LastName + ((client.EnrollDate != null) ?  "_" + DateTime.Parse(client.EnrollDate.ToString()).ToString("dd MM yyyy") : "") + ".docx";
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string documentPath = path + "Reports\\" + filename;
            string templatePath = path + "Reports" + "\\" + "CVTemplate.docx";

            using (DocX doc = DocX.Load(templatePath))
            {
                CV.CreateCurriculum(doc, client);
                doc.SaveAs(documentPath);
            }
            var bytes = System.IO.File.ReadAllBytes(documentPath);
            System.IO.File.Delete(documentPath);
            return File(bytes, "application/octet-stream", filename);
        }

        [HttpPost]
        public ActionResult ExportClients(ClientsFilter filters)
        {
            string filename = "Clientes.xls";
            
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string documentPath = path + "Reports\\" + filename;
            Reports.Helpers.Clients.GenerateReport(filename, filters);

            var bytes = System.IO.File.ReadAllBytes(documentPath);

            return new FilePathResult("~/Views/Clients/Index.html", "text/html");
        }

        [HttpPost]
        public ActionResult ExportClientsTracking(ClientsFilter filters)
        {
            string filename = "SeguimientoClientes.xls";

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string documentPath = path + "Reports\\" + filename;
            Reports.Helpers.Clients.GenerateClientsTracking(filename, filters);

            var bytes = System.IO.File.ReadAllBytes(documentPath);

            return new FilePathResult("~/Views/Clients/Index.html", "text/html");
        }

        [HttpGet]
        public ActionResult Download(string id)
        {
            string filename = id+".xls";
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string documentPath = path + "Reports\\" + filename;

            var bytes = System.IO.File.ReadAllBytes(documentPath);

            System.IO.File.Delete(documentPath);
            return File(bytes, "application/vnd.ms-excel", filename);
        }
    }
}
