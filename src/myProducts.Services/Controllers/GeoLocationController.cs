using Microsoft.AspNet.SignalR;
using MyProducts.Services.Hubs;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    [AllowAnonymous]
    public class GeoLocationController : ApiController
    {
        public void Post(dynamic loc)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            hub.Clients.All.sendLogEvent(loc);
        }
    }
}