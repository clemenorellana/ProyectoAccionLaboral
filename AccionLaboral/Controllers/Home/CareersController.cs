using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class CareersController : Controller
    {
        //
        // GET: /Careers/
        public ActionResult Index()
        {
            //return View();
            return new FilePathResult("~/Views/Careers/Index.html", "text/html");

        }

        //
        // GET: /Careers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Careers/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Careers/Create
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
        // GET: /Careers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Careers/Edit/5
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
        // GET: /Careers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Careers/Delete/5
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
