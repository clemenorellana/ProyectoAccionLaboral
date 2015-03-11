using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class VacantsByCompanyController : Controller
    {
        //
        // GET: /VacantsByCompany/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/VacantsByCompany/Index.html", "text/html");
        }


        //
        // GET: /VacantsByCompany/
        public ActionResult VacantList()
        {
            return new FilePathResult("~/Views/VacantsByCompany/VacantList.html", "text/html");
        }

        //
        // GET: /VacantsByCompany/Details/5
        public ActionResult Details(int id)
        {
            return new FilePathResult("~/Views/VacantsByCompany/Detail.html", "text/html");
        }

        //
        // GET: /VacantsByCompany/Create
        public ActionResult Create()
        {
            return new FilePathResult("~/Views/VacantsByCompany/Create.html", "text/html");
        }

        //
        // POST: /VacantsByCompany/Create
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
        // GET: /VacantsByCompany/Edit/5
        public ActionResult Edit(int id)
        {
            return new FilePathResult("~/Views/VacantsByCompany/Create.html", "text/html");
        }

        //
        // POST: /VacantsByCompany/Edit/5
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
        // GET: /VacantsByCompany/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /VacantsByCompany/Delete/5
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
