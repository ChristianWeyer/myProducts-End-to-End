using Microsoft.Owin.Security.OAuth;
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
            return;
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
            id.AddClaim(new Claim("urn:tt:app", true.ToString()));

            context.Validated(id);

            await Task.FromResult(0);
            return;
        }
    }
}
