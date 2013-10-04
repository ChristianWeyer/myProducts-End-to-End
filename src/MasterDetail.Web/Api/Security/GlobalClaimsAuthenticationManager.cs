using System.Security.Claims;

namespace MasterDetail.Web.Api.Security
{
    public class GlobalClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            // NOTE: Add custom logic here
            return base.Authenticate(resourceName, incomingPrincipal);
        }
    }
}
