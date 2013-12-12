using Fabrik.Common.WebAPI;
using MyProducts.Resources;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Metadata;
using Thinktecture.Applications.Framework.WebApi;
using Thinktecture.Applications.Framework.WebApi.Debugging;
using Thinktecture.Applications.Framework.WebApi.ModelMetadata;
using Thinktecture.IdentityModel.WebApi;
using WebApiContrib.Formatting;

namespace MyProducts.Hosting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; // ONLY for debugging

            config.Services.Replace(typeof(IContentNegotiator), new JsonOnlyContentNegotiator());

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Filters.Add(new ClaimsAuthorizeAttribute());

            config.MessageHandlers.Insert(0, new CompressionHandler());
            //config.MessageHandlers.Add(new PerfItDelegatingHandler(config, "myProducts application services"));

            config.Services.Replace(typeof(ModelMetadataProvider), 
                new ConventionalModelMetadataProvider(false, typeof(ValidationResources)));
        }
    }
}