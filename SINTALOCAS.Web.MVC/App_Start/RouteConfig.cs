using System.Web.Mvc;
using System.Web.Routing;

namespace SINTALOCAS.Web.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Afiliacao", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
