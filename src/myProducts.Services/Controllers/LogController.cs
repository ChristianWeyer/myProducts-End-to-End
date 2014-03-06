using MyProducts.Services.DTOs;
using Serilog;
using System.Web.Http;

namespace MyProducts.Services.Controllers
{
    /// <summary>
    /// Web API to provide logging data from clients.
    /// </summary>
    [AllowAnonymous]
    public class LogController : ApiController
    {
        /// <summary>
        /// Send log data.
        /// </summary>
        /// <param name="logData">Data to log</param>
        public void Post(LogData logData)
        {
            switch (logData.Level)
            {
                case "INFO":
                    Log.Logger.Information("ngmd: {@LogData}", logData);
                    break;
                case "ERROR":
                    Log.Logger.Error("ngmd: {@LogData}", logData);
                    break;
                default:
                    Log.Logger.Information("ngmd: {@LogData}", logData);
                    break;
            }
        }
    }
}