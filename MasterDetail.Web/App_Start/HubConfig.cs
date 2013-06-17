using System.Web.Routing;

namespace MasterDetail.Web.App_Start
{
    public class HubConfig
    {
        public static void Register(RouteCollection routes)
        {
            routes.MapHubs();
        }
    }
}