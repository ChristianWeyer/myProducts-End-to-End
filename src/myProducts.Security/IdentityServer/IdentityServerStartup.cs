using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.WsFederation;
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
                    SiteName = "ACME myProducts Identity Server",
                    Factory = factory,
                    SigningCertificate = Cert.Load(),

                    CorsPolicy = CorsPolicy.AllowAll,

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                    },

                    LoggingOptions = new LoggingOptions
                    {
                        //EnableHttpLogging = true, 
                        //EnableWebApiDiagnostics = true,
                        //IncludeSensitiveDataInLogs = true
                    },

                    EventsOptions = new EventsOptions
                    {
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true,
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true
                    }
                };

                idSrvApp.UseIdentityServer(idsrvOptions);
            });
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Google",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
                ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                Caption = "Facebook",
                SignInAsAuthenticationType = signInAsType,

                AppId = "676607329068058",
                AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            };
            app.UseFacebookAuthentication(fb);

            var adfs = new WsFederationAuthenticationOptions
            {
                AuthenticationType = "adfs",
                Caption = "ADFS",
                SignInAsAuthenticationType = signInAsType,

                MetadataAddress = "https://adfs.leastprivilege.vm/federationmetadata/2007-06/federationmetadata.xml",
                Wtrealm = "urn:idsrv3"
            };
            app.UseWsFederationAuthentication(adfs);

            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,

                Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705",
                ClientId = "65bbbda8-8b85-4c9d-81e9-1502330aacba",
                RedirectUri = "https://localhost:44333/core/aadcb"
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
    }
}
