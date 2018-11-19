using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DHDomtica
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
            name: "PageinationStore",
            url: "{controller}/{action}/{categoryId}/{pageId}",
            defaults: new { controller = "Store", action = "Pagination", categoryId = UrlParameter.Optional, pageId = UrlParameter.Optional }
            );
        }
    }
}
