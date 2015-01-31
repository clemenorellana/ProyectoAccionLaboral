using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers.Home
{
    public class InterviewTypesController : Controller
    {
        //
        // GET: /InterviewTypes/
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/InterviewTypes/Index.html", "text/html");
        }

        //
        // GET: /InterviewTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /InterviewTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InterviewTypes/Create
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
        // GET: /InterviewTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /InterviewTypes/Edit/5
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
        // GET: /InterviewTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /InterviewTypes/Delete/5
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
