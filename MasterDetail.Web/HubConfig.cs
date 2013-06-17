using System.Web.Routing;

namespace MasterDetail.Web
{
    public class HubConfig
    {
        public static void Register(RouteCollection routes)
        {
            routes.MapHubs();
        }
    }
}