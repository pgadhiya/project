﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            //name: "Default",
            //url: "{controller}/{action}/{id}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Admin", action = "Defaulthome", id = UrlParameter.Optional }
            //"Area",
            //"{area}/",
            //new { area = "admin", controller = "Admin ", action = "Login " }
            ).DataTokens.Add("area", "admin");
        }
    }
}
