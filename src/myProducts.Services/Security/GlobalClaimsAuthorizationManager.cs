using System;
using System.Security.Claims;

namespace MyProducts.Services.Security
{
    public class GlobalClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var ttApp = ClaimsPrincipal.Current.FindFirst("urn:tt:app");

                return ttApp != null && Convert.ToBoolean(ttApp.Value);
            }

            return false;
        }
    }
}
