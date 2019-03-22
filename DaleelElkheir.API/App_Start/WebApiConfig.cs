using DaleelElkheir.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace DaleelElkheir.API
{
    public static class WebApiConfig
    {
        public static UnityContainer container = new UnityContainer();
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //config.EnableCors();

            IocConfigurator.RegisterServices(container);
            config.DependencyResolver = new APIResolver(container);

            //Auto Mapper Configuration
          //  WebApiApplication.Configure();


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
        }
    }
}
