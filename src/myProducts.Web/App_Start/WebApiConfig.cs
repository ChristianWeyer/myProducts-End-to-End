using Fabrik.Common.WebAPI;
using Microsoft.AspNet.SignalR;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using MyProducts.Web.Api.Hubs;
using PerfIt;
using Serilog;
using System;
using System.IdentityModel.Services;
using System.Net.Http.Formatting;
using System.Web.Http;
using Thinktecture.IdentityModel.Authorization.WebApi;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MyProducts.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var logConfig = new LoggerConfiguration()
                .WriteTo.SignalR(GlobalHost.ConnectionManager.GetHubContext<LogHub>());
               
            try
            {
                var setting = CloudConfigurationManager.GetSetting("StorageConnectionString");
                var storage = CloudStorageAccount.Parse(setting);
                logConfig.WriteTo.AzureTableStorage(storage);
            }
            catch{}

            Log.Logger = logConfig.CreateLogger();;

            config.IncludeErrorDetailPolicy =
                IncludeErrorDetailPolicy.Always;
            config.EnableSystemDiagnosticsTracing();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.EnableQuerySupport();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var authNConfig = new AuthenticationConfiguration
            {
                RequireSsl = true,
                EnableSessionToken = true,
                SessionToken = new SessionTokenConfiguration()
                {
                    DefaultTokenLifetime = TimeSpan.FromHours(24),
                    SigningKey = Convert.FromBase64String("V5cgP0Bzx4goMmOrFKUIPqUWRNmgpoH8IxXQ92M2T0E=")
                },
                SendWwwAuthenticateResponseHeaders = false,
                ClaimsAuthenticationManager = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager
            };

            authNConfig.AddBasicAuthentication(
                (un, pw) => un == pw); // this is the super complex basic authentication validation logic :)

            config.MessageHandlers.Add(new AuthenticationHandler(authNConfig));
            config.Filters.Add(new ClaimsAuthorizeAttribute());

            config.MessageHandlers.Insert(0, new CompressionHandler());
            //config.MessageHandlers.Add(new PerfItDelegatingHandler(config, "myProducts application services"));
        }
    }
}