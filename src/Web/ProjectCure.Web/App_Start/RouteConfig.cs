using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectCure.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.json/{*pathInfo}");
            routes.IgnoreRoute("{resource}.css/{*pathInfo}");
            routes.IgnoreRoute("{resource}.js/{*pathInfo}");

            routes.MapRoute(
                name: "CalendarEvents",
                url: "events/",
                defaults: new { controller = "Events", action = "List" }
            );

            routes.MapRoute(
                name: "CalendarEvent",
                url: "events/{id}",
                defaults: new { controller = "Events", action = "Item" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}