using BooksWebapi.App_Start;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BooksWebapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

           
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(
                0, new IsoDateTimeConverter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //自定义路由
            config.Routes.MapHttpRoute(
              name: "actionApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
        }
    }
}
