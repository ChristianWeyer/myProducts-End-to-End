using System.Collections.Generic;
using System.IdentityModel.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Models;

namespace MyProducts.Security.IdentityServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
                {
                    new Scope
                    {
                        Name = "default",
                        DisplayName = "Default application scope",
                        Type = ScopeType.Resource,
                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim(Constants.ClaimTypes.Name)
                        }
                    }
                };
        }
    }
}