using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyProducts.Security;
using MyProducts.Security.IdentityServer;
using Owin;
using System;

namespace MyProducts.Hosting
{
    public static class SecurityStartup
    {
        public static void Configuration(IAppBuilder app)
        {
            var idSrv = new IdentityServerStartup();
            idSrv.Configuration(app);

            //app.UseCors(CorsOptions.AllowAll);

            //app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
            //    Provider = new AuthorizationServerProvider()
            //});
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new EnhancedAuthenticationProvider(),
                AuthenticationMode = AuthenticationMode.Passive
            });
        }
    }
}