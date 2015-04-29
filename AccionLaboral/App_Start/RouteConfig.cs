using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AccionLaboral
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "register",
                url: "{controller}/{action}/{userName}/{token}",
                defaults: new { controller = "Home", action = "Index", userName = UrlParameter.Optional, token = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Download",
                url: "{controller}/{action}/{filename}",
                defaults: new { controller = "Home", action = "Index", filename = UrlParameter.Optional }
            );
        }
    }
}
