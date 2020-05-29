using Microsoft.Owin.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CustomerWidgetMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes

            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*", "*", "*", "Authorization");
           config.EnableCors(cors);
        


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            //config.EnableCors(cors);
        }
    }
}
