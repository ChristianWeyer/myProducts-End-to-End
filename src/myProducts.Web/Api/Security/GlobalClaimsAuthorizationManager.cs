using System.Security.Claims;

namespace MyProducts.Web.Api.Security
{
    public class GlobalClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                // NOTE: Add custom logic here
                return base.CheckAccess(context);
            }

            return false;
        }
    }
}
