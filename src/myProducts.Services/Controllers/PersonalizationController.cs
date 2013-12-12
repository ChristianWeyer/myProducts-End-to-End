using MyProducts.Web.Api.DTOs.Personalization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    public class PersonalizationController : ApiController
    {
        public PersonalizationData GetPersonalizationData()
        {
            var user = this.RequestContext.Principal;
 
            var persData = new PersonalizationData
                {
                    Features = GetFeatures(user).ToList(),
                    UiClaims = new UiClaimsData
                        {
                            UserName = user.Identity.Name,
                            Capabilities = GetCapabilities(user),
                            Constraints = GetConstraints(user),
                            NameValueClaims = GetNameValueClaims(user)
                        }
                };

            return persData;
        }

        private IEnumerable<FeatureItem> GetFeatures(IPrincipal principal)
        {
            var module0 = new FeatureItem { Module = "Articles", DisplayText = "INDEX_ARTICLES", Url = "/articles", MatchPattern = "(/article.*)", Users = new List<string> { "cw", "bob" } };
            var module1 = new FeatureItem { Module = "ArticleDetails", Url = "/articledetails/:id", Users = new List<string> { "cw", "bob" } };
            var module2 = new FeatureItem { Module = "Gallery", DisplayText = "INDEX_GALLERY", Url = "/gallery", MatchPattern = "/gallery", Users = new List<string> { "cw" } };
            var module3 = new FeatureItem { Module = "Log", DisplayText = "INDEX_LOGS", Url = "/log", MatchPattern = "/log", Users = new List<string> { "cw" } };
            var module4 = new FeatureItem { Module = "Statistics", DisplayText = "INDEX_STATS", Url = "/stats", MatchPattern = "/stats", Users = new List<string> { "cw", "bob" } };

            return new List<FeatureItem> { module0, module1, module2, module3, module4 }.Where(m => m.Users.Contains(principal.Identity.Name));
        }

        private Constraints GetConstraints(IPrincipal principal)
        {
            double itemsLimit = GetItemsLimit(principal.Identity.Name);

            return new Constraints
            {
                new NumericConstraint
                {
                    Name = "MaxNumberOfItems",
                    LowerLimit = 0,
                    UpperLimit = itemsLimit
                }
            };
        }

        private double GetItemsLimit(string userName)
        {
            if (userName == "cw")
            {
                return 10;
            }

            return 5;
        }

        private Capabilities GetCapabilities(IPrincipal principal)
        {
            if (principal.Identity.Name.Equals("cw"))
            {
                return new Capabilities
                {
                    "AddArticle"
                };
            }

            return new Capabilities { };
        }

        private NameValueClaims GetNameValueClaims(IPrincipal principal)
        {
            if (principal.Identity.Name.Equals("cw"))
            {
                return new NameValueClaims
                {
                    new NameValueClaim("Email", "christian.weyer@thinktecture.com"),
                    new NameValueClaim("GivenName", "Christian"),
                };
            }

            return new NameValueClaims
                {
                    new NameValueClaim("Email", "na@thinktecture.com"),
                    new NameValueClaim("GivenName", "N/A"),
                };
        }
    }
}