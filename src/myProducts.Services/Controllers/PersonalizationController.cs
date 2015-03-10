using MyProducts.Web.Api.DTOs.Personalization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// NOTE: This is a hard-coded sample - usually the data would sit in a DB!
    /// Web API to deliver personalized application features and UI claims based on incoming user identity.
    /// </summary>
    public class PersonalizationController : ApiController
    {
        /// <summary>
        /// Get application personalization information based on the incoming user.
        /// </summary>
        /// <returns></returns>
        public PersonalizationData GetPersonalizationData()
        {
            var user = RequestContext.Principal as ClaimsPrincipal;
            string userName = user.FindFirst("sub").Value;
            
            var persData = new PersonalizationData
                {
                    Features = GetFeatures(userName).ToList(),
                    UiClaims = new UiClaimsData
                        {
                            UserName = userName,
                            Capabilities = GetCapabilities(userName),
                            Constraints = GetConstraints(userName),
                            NameValueClaims = GetNameValueClaims(userName)
                        }
                };

            return persData;
        }

        private IEnumerable<FeatureItem> GetFeatures(string userName)
        {
            var module0 = new FeatureItem { Module = "Articles", DisplayText = "INDEX_ARTICLES", Url = "/articles", MatchPattern = "(/article.*)", Users = new List<string> { "cw", "bob" } };
            var module1 = new FeatureItem { Module = "ArticleDetails", Url = "/articledetails/:id", Users = new List<string> { "cw", "bob" } };
            var module2 = new FeatureItem { Module = "Gallery", DisplayText = "INDEX_GALLERY", Url = "/gallery", MatchPattern = "/gallery", Users = new List<string> { "cw" } };
            var module3 = new FeatureItem { Module = "Log", DisplayText = "INDEX_LOGS", Url = "/log", MatchPattern = "/log", Users = new List<string> { "cw" } };
            var module4 = new FeatureItem { Module = "Statistics", DisplayText = "INDEX_STATS", Url = "/statistics", MatchPattern = "/statistics", Users = new List<string> { "cw", "bob" } };

            return new List<FeatureItem> { module0, module1, module2, module3, module4 }.Where(m => m.Users.Contains(userName));
        }

        private Constraints GetConstraints(string userName)
        {
            double itemsLimit = GetItemsLimit(userName);

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

        private Capabilities GetCapabilities(string userName)
        {
            if (userName.Equals("cw"))
            {
                return new Capabilities
                {
                    "AddArticle"
                };
            }

            return new Capabilities();
        }

        private NameValueClaims GetNameValueClaims(string userName)
        {
            if (userName.Equals("cw"))
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