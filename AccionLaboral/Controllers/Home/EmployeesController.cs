using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class EmployeesController : Controller
    {
        //
        // GET: /Employees/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/Employees/Index.html", "text/html");
        }

        //
        // GET: /Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employees/Create
        public ActionResult Create()
        {
            return new FilePathResult("~/Views/Employees/Create.html", "text/html");
        }

        //
        // POST: /Employees/Create
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
        // GET: /Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return new FilePathResult("~/Views/Employees/Create.html", "text/html");
        }

        //
        // POST: /Employees/Edit/5
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
        // GET: /Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employees/Delete/5
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
