using System.IdentityModel.Services;
using System.Net.Http.Formatting;
using System.Web.Http;
using Fabrik.Common.WebAPI;
using Thinktecture.IdentityModel.Authorization.WebApi;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MasterDetail.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy =
                IncludeErrorDetailPolicy.LocalOnly;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var authNConfig = new AuthenticationConfiguration
            {
                EnableSessionToken = true,
                SessionToken = new SessionTokenConfiguration()
                {
                    DefaultTokenLifetime = System.TimeSpan.FromHours(24)
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