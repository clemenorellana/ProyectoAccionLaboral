using System;
using System.Collections.Generic;
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


        //
        // GET: /Clients/Enroll/
        public ActionResult Enroll()
        {
            return new FilePathResult("~/Views/Clients/EnrollClient.html", "text/html");
        }
    }
}
