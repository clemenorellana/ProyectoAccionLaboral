using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccionLaboral.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize]
        public ActionResult Index()
        {
            return new FilePathResult("/Views/Home/Index.html", "text/html");
        }

        public ActionResult RegisterClient()
        {
            ViewBag.Message = "Your application description page.";

            return new FilePathResult("/Views/Clientes/RegisterClient.html", "text/html");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}