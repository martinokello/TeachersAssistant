using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace  TeacherAssistant
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
/*
            routes.MapRoute(
                name: "Grammar11Plus",
                url: "Areas/Grammar11Plus/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional                    },
                namespaces: new []{ "TeacherAssistant.Areas.Grammar11Plus.Controllers" }
            );
            routes.MapRoute(
                name: "StatePrimary",
                url: "Areas/StatePrimary/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new [] {  "TeacherAssistant.Areas.StatePrimary.Controllers" }
            );
            routes.MapRoute(
                name: "StateJunior",
                url: "Areas/StateJunior/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[]{"TeacherAssistant.Areas.StateJunior.Controllers" }
            );
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TeacherAssistant.Controllers" }
            );
        }
    }
}
