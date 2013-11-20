using System;
using System.IdentityModel.Services;
using System.Web.Http;
using Thinktecture.IdentityModel.Authorization.WebApi;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MyProducts.Hosting
{
    public static class SecurityConfig
    {
        public static void Register(HttpConfiguration config)
        {
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
                    ClaimsAuthenticationManager =
                        FederatedAuthentication.FederationConfiguration.IdentityConfiguration
                                               .ClaimsAuthenticationManager
                };

            authNConfig.AddBasicAuthentication(
                (un, pw) => un == pw); // this is the super complex basic authentication validation logic :)

            config.MessageHandlers.Add(new AuthenticationHandler(authNConfig));
            config.Filters.Add(new ClaimsAuthorizeAttribute());
        }
    }
}