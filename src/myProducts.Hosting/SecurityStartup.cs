using System.Collections.Generic;
using System.IdentityModel.Tokens;
using MyProducts.Security;
using MyProducts.Security.Config;
using Owin;
using Thinktecture.IdentityServer.AccessTokenValidation;

namespace MyProducts.Hosting
{
    public static class SecurityStartup
    {
        public static void Configuration(IAppBuilder app)
        {
            var idSrv = new IdentityServerStartup();
            idSrv.Configuration(app);

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            { 
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://idsrv.acme.com",
                IssuerCertificate = Cert.Load()
            });
        }
    }
}