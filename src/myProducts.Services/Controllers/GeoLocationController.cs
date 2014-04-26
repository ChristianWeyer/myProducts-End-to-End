using System.Web.Http;
using Serilog;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API to accept geo location data for tracking.
    /// </summary>
    [AllowAnonymous]
    public class GeoLocationController : ApiController
    {
        /// <summary>
        /// Post a dynamic data structure with geo location data.
        /// </summary>
        /// <param name="loc"></param>
        public void Post(dynamic loc)
        {
            Log.Logger.Information("ngmd: {@Location}", loc);
        }
    }
}