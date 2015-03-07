using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace MyProducts.Security.IdentityServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
                {
                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    StandardScopes.Email,
                    StandardScopes.Address,
                    StandardScopes.OfflineAccess,
                    StandardScopes.RolesAlwaysInclude,
                    StandardScopes.AllClaims,

                    new Scope
                    {
                        Name = "read",
                        DisplayName = "Read data",
                        Type = ScopeType.Resource,
                        Emphasize = false,
                    },
                    new Scope
                    {
                        Name = "write",
                        DisplayName = "Write data",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                    }
                };
        }
    }
}