using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Http;
using MasterDetail.Web.Api.DTOs;
using MasterDetail.Web.Api.DTOs.Personalization;

namespace MasterDetail.Web.Api.Controllers
{
    public class PersonalizationController : ApiController
    {
        public PersonalizationData GetPersonalizationData()
        {
            var persData = new PersonalizationData
                {
                    Features = GetFeatures(User).ToList(),
                    UiClaims = new UiClaimsData
                        {
                            UserName = User.Identity.Name,
                            Capabilities = GetCapabilities(User),
                            Constraints = GetConstraints(User),
                            NameValueClaims = GetNameValueClaims(User)
                        }
                };

            return persData;
        }

        private IEnumerable<FeatureItem> GetFeatures(IPrincipal principal)
        {
            var module0 = new FeatureItem { Module = "Articles", DisplayText = "INDEX_ARTICLES", Url = "/articles", MatchPattern = "(/|/articles.*)", Users = new List<string> { "cw", "bob" } };
            var module1 = new FeatureItem { Module = "ArticleDetails", Url = "/articledetails/:id", Users = new List<string> { "cw", "bob" } };
            var module2 = new FeatureItem { Module = "Admin", DisplayText = "INDEX_ADMIN", Url = "/admin", MatchPattern = "/admin", Users = new List<string> { "cw" } };
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