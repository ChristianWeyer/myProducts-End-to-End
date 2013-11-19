using System.Collections.Generic;

namespace MyProducts.Web.Api.DTOs.Personalization
{
    public class PersonalizationData
    {
        public IEnumerable<FeatureItem> Features { get; set; }
        public UiClaimsData UiClaims { get; set; }
    }
}