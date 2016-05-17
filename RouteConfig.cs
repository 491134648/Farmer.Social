using System.Web.Mvc;
using System.Web.Routing;
using MVCForum.Domain.Constants;

namespace MVCForum.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.LowercaseUrls = true;
            RouteTable.Routes.AppendTrailingSlash = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            //.RouteHandler = new SlugRouteHandler()
        }
    }
}
