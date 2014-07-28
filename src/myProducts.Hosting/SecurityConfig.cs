using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyProducts.Security;
using Owin;
using System;

namespace MyProducts.Hosting
{
    public static class SecurityConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new AuthorizationServerProvider()
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new EnhancedAuthenticationProvider(),
                AuthenticationMode = AuthenticationMode.Passive
            });
        }
    }
}