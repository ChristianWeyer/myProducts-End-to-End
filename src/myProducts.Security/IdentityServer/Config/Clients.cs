using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace MyProducts.Security.IdentityServer.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName  = "myProducts RO Client",
                    Enabled = true,

                    ClientId = "myp-roclient",
                    ClientSecrets = new List<ClientSecret>
                    { 
                        new ClientSecret("not-a-secret".Sha256())
                    },

                    Flow=Flows.ResourceOwner,

                    AccessTokenLifetime = 3600
                },
                new Client
                {
                    ClientName = "myProducts Implicit Client",
                    Enabled = true,

                    ClientId = "myp-implicitclient",
                    ClientSecrets = new List<ClientSecret>
                    { 
                        new ClientSecret("secret".Sha256())
                    },

                    Flow = Flows.Implicit,
                    
                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:23453/callback.html",
                        "http://localhost:23453/frame.html",
                        "http://localhost:23453/modal.html"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:23453/index.html"
                    },
                    
                    IdentityTokenLifetime = 360,
                    AccessTokenLifetime = 3600
                }
            };
        }
    }
}