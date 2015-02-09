using System.Web.Http.Cors;
using Fabrik.Common.WebAPI;
using Microsoft.Owin.Security.OAuth;
using MyProducts.Resources;
using System.Web.Http;
using System.Web.Http.Metadata;
using Thinktecture.Applications.Framework.WebApi;
using Thinktecture.Applications.Framework.WebApi.ModelMetadata;
using Thinktecture.IdentityModel.WebApi;

namespace MyProducts.Hosting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.UseJsonOnly();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            //config.Filters.Add(new ResourceActionAuthorizeAttribute());

            config.MessageHandlers.Insert(0, new CompressionHandler());
            //config.MessageHandlers.Add(new PerfItDelegatingHandler(config, "myProducts application services"));

            config.Services.Replace(typeof(ModelMetadataProvider),
                new ConventionalModelMetadataProvider(false, typeof(ValidationResources)));

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            var resolver = IocConfiguration.ConfigureContainerForWebApi();
            config.DependencyResolver = resolver;
        }
    }
}