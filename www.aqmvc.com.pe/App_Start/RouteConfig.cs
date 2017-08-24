using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace www.aqmvc.com.pe
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cuenta", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
