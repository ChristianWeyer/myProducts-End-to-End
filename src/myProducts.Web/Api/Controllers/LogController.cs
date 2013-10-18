using System.Web.Http;
using MyProducts.Web.Api.DTOs;
using Serilog;

namespace MyProducts.Web.Api.Controllers
{
    [AllowAnonymous]
    public class LogController : ApiController
    {
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