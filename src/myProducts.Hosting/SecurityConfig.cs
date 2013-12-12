using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.IdentityModel.Services;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;
using Thinktecture.IdentityModel.WebApi.Authentication.Handler;

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
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}