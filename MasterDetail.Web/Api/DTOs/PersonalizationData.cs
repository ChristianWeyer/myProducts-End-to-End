using System.Collections.Generic;

namespace MasterDetail.Web.Api.DTOs
{
    public class PersonalizationData
    {
        public List<FeatureItem> Features { get; set; }
        public UiClaimsData UiClaims { get; set; }
    }
}