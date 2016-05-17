using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Farmer.Social.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // API Attribute Routes
            //routes.MapMvcAttributeRoutes();wechatconnect
            routes.MapRoute(
              "wechatconnect", // Route name
             "wechatconnect", // URL with parameters
              new { controller = "Wechat", action = "CallBack", id = UrlParameter.Optional } // Parameter defaults
          );
            routes.MapRoute(
              "weiboconnect", // Route name
             "weiboconnect", // URL with parameters
              new { controller = "Weibo", action = "CallBack", id = UrlParameter.Optional } // Parameter defaults
          );
            routes.MapRoute(
              "qqconnect", // Route name
             "qqconnect", // URL with parameters
              new { controller = "QQOAuth", action = "CallBack", id = UrlParameter.Optional } // Parameter defaults
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
