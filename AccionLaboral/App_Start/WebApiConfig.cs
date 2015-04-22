using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AccionLaboral
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.EnableCors();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ExportApi",
                routeTemplate: "api/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //string searchTerm, string searchField, bool? searchDefault, int? limit

            config.Routes.MapHttpRoute(
            name: "SearchApi",
            routeTemplate: "api/{action}/{searchTerm}/{searchField}/{limit}",
            defaults: new { limit = RouteParameter.Optional }
        );

            config.Routes.MapHttpRoute(
                name: "ClientsByEmployee",
                routeTemplate: "api/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TrackingApi",
                routeTemplate: "api/{controller}/{id}/Tracking",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
    }
}
