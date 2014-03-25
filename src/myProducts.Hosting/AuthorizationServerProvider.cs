using Microsoft.Owin.Security.OAuth;
using MyProducts.Services.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyProducts.Hosting
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            await Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // TODO: real authN - this is for demo only ;)
            if (context.UserName != context.Password)
            {
                context.Rejected();

                await Task.FromResult(0);
                return;
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            id.AddClaim(new Claim(ApplicationClaimTypes.Default, ApplicationClaimsValues.Present));

            // TODO: Again, hard-coded demo scenario!
            if (context.UserName == "cw")
            {
                id.AddClaim(new Claim(ApplicationClaimTypes.Maintenance, ApplicationClaimsValues.Editor));
            }

            context.Validated(id);

            await Task.FromResult(0);
        }
    }
}
