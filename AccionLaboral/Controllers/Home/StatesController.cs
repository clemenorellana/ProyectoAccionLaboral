using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class StatesController : Controller
    {
        //
        // GET: /States/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/States/Index.html", "text/html");
        }

        //
        // GET: /States/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /States/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /States/Create
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
        // GET: /States/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /States/Edit/5
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
        // GET: /States/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /States/Delete/5
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
