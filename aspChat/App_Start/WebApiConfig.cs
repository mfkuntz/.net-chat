using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using aspChat.Controllers;
using Newtonsoft.Json.Serialization;

namespace aspChat
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ChatSocket",
                routeTemplate: "socket.io",
                defaults: new {controller = "socket"}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new  { id = @"\w+"}
            );

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
