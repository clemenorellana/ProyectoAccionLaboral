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
            return new FilePathResult("~/Views/Shared/_Layout.html", "text/html");   
        }

        public ActionResult Home()
        {
            return new FilePathResult("~/Views/Home/Index.html", "text/html");
        }

        public ActionResult RegisterClient(string userName, string token)
        {
            return new FilePathResult("~/Views/Accounts/ChangePassword.html", "text/html");
        }

        public ActionResult RegisterUser()
        {
            return new FilePathResult("~/Views/Home/HomePage.html", "text/html");
        }

        public ActionResult HomePage()
        {
            return new FilePathResult("~/Views/Home/HomePage.html", "text/html");
        }

    }
}