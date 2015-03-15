using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class CompaniesController : Controller
    {
        //
        // GET: /Companies/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/Companies/Index.html", "text/html");
        }

        
        public ActionResult NewCompaniesReport()
        {
            return new FilePathResult("~/Views/Reports/NewCompaniesReport.html", "text/html");
        }

        //
        // GET: /Companies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Companies/Create
        public ActionResult Create()
        {
            return new FilePathResult("~/Views/Companies/Create.html", "text/html");
        }

        //
        // POST: /Companies/Create
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
        // GET: /Companies/Edit/5
        public ActionResult Edit(int id)
        {
            return new FilePathResult("~/Views/Companies/Create.html", "text/html");
        }

        //
        // POST: /Companies/Edit/5
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
        // GET: /Companies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Companies/Delete/5
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
    }
}
