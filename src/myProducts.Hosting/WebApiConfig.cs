using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Fabrik.Common.WebAPI;
using Microsoft.Owin.Security.OAuth;
using MyProducts.Resources;
using System.Net.Http.Formatting;
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
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; // TODO: ONLY for debugging

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Services.Replace(typeof(IContentNegotiator), new JsonOnlyContentNegotiator());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Filters.Add(new ResourceActionAuthorizeAttribute());

            config.MessageHandlers.Insert(0, new CompressionHandler());
            //config.MessageHandlers.Add(new PerfItDelegatingHandler(config, "myProducts application services"));

            config.Services.Replace(typeof(ModelMetadataProvider), 
                new ConventionalModelMetadataProvider(false, typeof(ValidationResources)));

            var resolver = ConfigureContainer();
            config.DependencyResolver = resolver;
        }

        private static AutofacWebApiDependencyResolver ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            var webApiAssembly = Assembly.Load("MyProducts.Services");
            builder.RegisterApiControllers(webApiAssembly);

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }
    }
}