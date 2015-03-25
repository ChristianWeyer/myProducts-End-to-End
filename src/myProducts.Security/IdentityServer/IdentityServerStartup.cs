using MyProducts.Security.IdentityServer.Config;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;

namespace MyProducts.Security.IdentityServer
{
    public class IdentityServerStartup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map("/idsrv", idSrvApp =>
            {
                var factory = InMemoryFactory.Create(
                    users: Users.Get(),
                    clients: Clients.Get(),
                    scopes: Scopes.Get());

                factory.ConfigureClientStoreCache();
                factory.ConfigureScopeStoreCache();
                factory.ConfigureUserServiceCache();

                var idsrvOptions = new IdentityServerOptions
                {
                    SiteName = "ACME Identity Server",
                    IssuerUri = "https://idsrv.acme.com",
                    Factory = factory,
                    SigningCertificate = Cert.Load(),

                    CorsPolicy = CorsPolicy.AllowAll,
                };

                idSrvApp.UseIdentityServer(idsrvOptions);
            });
        }
    }
}
