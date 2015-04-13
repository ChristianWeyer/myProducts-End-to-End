using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace MyProducts.Security.Config
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

                    AccessTokenLifetime = 86400
                },
                new Client
                {
                    ClientName  = "myProducts SPA Client",
                    Enabled = true,

                    ClientId = "myp-implicitclient",
                    ClientSecrets = new List<ClientSecret>
                    { 
                        new ClientSecret("not-a-secret".Sha256())
                    },

                    Flow=Flows.Implicit,

                    RequireConsent = false,

                    RedirectUris = new List<string>
                    {
                        "https://localhost/ngmd/client/",
                    },
                    AccessTokenLifetime = 86400
                }
            };
        }
    }
}