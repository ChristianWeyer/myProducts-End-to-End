using System.Collections.Generic;

namespace MasterDetail.Web.Api.DTOs
{
    public class Features : List<FeatureItem>
    { }

    public class FeatureItem
    {
        public string Module { get; set; }
        public string Url { get; set; }
        public bool OverrideRoot { get; set; }
        public string DisplayText { get; set; }
        public string MatchPattern { get; set; }
        // TODO: this must not be exosed to the clients
        public List<string> Users { get; set; }
    }
}