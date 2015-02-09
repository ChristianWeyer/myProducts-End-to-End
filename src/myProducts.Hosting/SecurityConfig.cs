using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyProducts.Security;
using Owin;
using System;
using Microsoft.Owin.Security.DataProtection;

namespace MyProducts.Hosting
{
    public static class SecurityConfig
    {
        public static void Register(IAppBuilder app)
        {
			app.SetDataProtectionProvider(new AesDataProtectorProvider("123456789"));

            app.UseCors(CorsOptions.AllowAll);
		
		    app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new AuthorizationServerProvider(),
				
					AllowInsecureHttp = true
            });
        
			/*
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new EnhancedAuthenticationProvider(),
                AuthenticationMode = AuthenticationMode.Passive
            });
            */
        }
    }
}