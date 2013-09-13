using System.Collections.Generic;
using System.Web.Http;

namespace MasterDetail.Web.Api.Controllers
{
    public class NavigationController : ApiController
    {
        [HttpGet]
        [ActionName("list")]
        public List<NavigationItem> ListNavigationItems()
        {
            // TODO: check against repo with current user

            var navItem1 = new NavigationItem { DisplayText = "Administration", Url = "/admin", MatchPattern = "/admin.*" };
            var navItem2 = new NavigationItem { DisplayText = "Logs", Url = "/log", MatchPattern = "/log" };
            var navItemList = new List<NavigationItem> { navItem1, navItem2 };

            return navItemList;
        }
    }

    public class NavigationItem
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
        public string MatchPattern { get; set; }
    }
}