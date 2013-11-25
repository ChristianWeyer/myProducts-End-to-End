using Fabrik.Common.WebAPI;
using System;
using System.IdentityModel.Services;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MyProducts.Hosting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; // ONLY for debugging
            config.EnableSystemDiagnosticsTracing();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.EnableQuerySupport();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.MessageHandlers.Insert(0, new CompressionHandler());
            //config.MessageHandlers.Add(new PerfItDelegatingHandler(config, "myProducts application services"));
        }
    }
}