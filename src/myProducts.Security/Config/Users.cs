using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace MyProducts.Security.Config
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var users = new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "cw", Username = "cw", Password = "cw", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Christian Weyer"),
                        new Claim(Constants.ClaimTypes.GivenName, "Christian"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Weyer"),
                        new Claim(Constants.ClaimTypes.Email, "christian.weyer@thinktecture.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Admin")                    }
                },
                new InMemoryUser{Subject = "bob", Username = "bob", Password = "bob", 
                    Claims = new Claim[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Bob Smith"),
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(Constants.ClaimTypes.Role, "Developer")
                    }
                },
            };

            return users;
        }
    }
}