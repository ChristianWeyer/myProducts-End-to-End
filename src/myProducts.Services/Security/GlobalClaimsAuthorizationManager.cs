using System;
using System.Security.Claims;
using Thinktecture.IdentityModel.Extensions;

namespace MyProducts.Services.Security
{
    public class GlobalClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var ttApp = ClaimsPrincipal.Current.FindFirst("urn:tt:app");

                if(ttApp != null && Convert.ToBoolean(ttApp.Value) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
