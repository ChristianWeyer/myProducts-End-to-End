using Microsoft.AspNet.SignalR;
using Serilog;

namespace MyProducts.Hosting
{
    public static class LoggingConfig
    {
        public static void Configure()
        {
            var logConfig = new LoggerConfiguration()
                .WriteTo.SignalR(GlobalHost.ConnectionManager.GetHubContext<LoggingHub>());

            Log.Logger = logConfig.CreateLogger();
        }
    }
}