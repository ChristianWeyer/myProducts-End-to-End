using MyProducts.Security.IdentityServer;
using MyProducts.Security.IdentityServer.Config;
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

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://idsrv.acme.com",
                IssuerCertificate = Cert.Load()

            });
        }
    }
}