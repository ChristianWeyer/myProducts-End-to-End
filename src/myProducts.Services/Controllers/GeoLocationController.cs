using Microsoft.AspNet.SignalR;
using MyProducts.Services.Hubs;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API to accept geo location data for tracking.
    /// </summary>
    [AllowAnonymous]
    public class GeoLocationController : ApiController
    {
        /// <summary>
        /// Post a dynamic data structure with geo location data. Currently not persisted but sent out to clients via logging Hub
        /// </summary>
        /// <param name="loc"></param>
        public void Post(dynamic loc)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            hub.Clients.All.sendLogEvent(loc);
        }
    }
}