using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace MyProducts.Security
{
    public class EnhancedAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (context.Token == null)
            {
                var value = context.Request.Query.Get("access_token");

                if (!string.IsNullOrWhiteSpace(value))
                {
                    context.Token = value;
                }

                return Task.FromResult(0);
            }
            
            return base.RequestToken(context);
        }
    }
}
