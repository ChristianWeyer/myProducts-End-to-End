using System.Web.Http;
using Serilog;

namespace MasterDetail.Web.Api.Controllers
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

    public class LogData
    {
        public string Logger { get; set; }
        public string Timestamp { get; set; }
        public string Level { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}