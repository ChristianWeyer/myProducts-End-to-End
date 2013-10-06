using System;
using System.IdentityModel.Services;
using System.Net.Http.Formatting;
using System.Web.Http;
using Fabrik.Common.WebAPI;
using MasterDetail.Web.Api.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Serilog;
using Thinktecture.IdentityModel.Authorization.WebApi;
using Thinktecture.IdentityModel.Http.Cors;
using Thinktecture.IdentityModel.Http.Cors.WebApi;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MasterDetail.Web.App_Start
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
                IncludeErrorDetailPolicy.LocalOnly;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.EnableQuerySupport();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var corsConfig = new CorsConfiguration();
            corsConfig.AllowAll();
            var corsHandler = new CorsMessageHandler(corsConfig, config);
            config.MessageHandlers.Add(corsHandler);

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
        }
    }
}