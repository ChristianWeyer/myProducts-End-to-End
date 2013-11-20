using MyProducts.Services.DTOs;
using Serilog;
using System.Web.Http;

namespace MyProducts.Services.Controllers
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