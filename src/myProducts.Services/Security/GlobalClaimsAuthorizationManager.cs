using System.Linq;
using System.Security.Claims;

namespace MyProducts.Services.Security
{
    public class GlobalClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            if (!ClaimsPrincipal.Current.Identity.IsAuthenticated) return false;
            
            if (!ClaimsPrincipal.Current.HasClaim(ApplicationClaimTypes.Default, ApplicationClaimsValues.Present)) return false;

            if (context.Action.FirstOrDefault(ac => ac.Value == "Save") != null &&
                context.Resource.FirstOrDefault(rc => rc.Value == "Article") != null &&
                !ClaimsPrincipal.Current.HasClaim(ApplicationClaimTypes.Maintenance, ApplicationClaimsValues.Editor))
            {
                return false;
            }

            return true;
        }
    }
}
