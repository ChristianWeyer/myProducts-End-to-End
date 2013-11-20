using Microsoft.AspNet.SignalR;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Serilog;

namespace MyProducts.Hosting
{
    public static class LoggingConfig
    {
        private static void Configure()
        {
            var logConfig = new LoggerConfiguration()
                .WriteTo.SignalR(GlobalHost.ConnectionManager.GetHubContext<LogHub>());

            try
            {
                var setting = CloudConfigurationManager.GetSetting("StorageConnectionString");
                var storage = CloudStorageAccount.Parse(setting);
                logConfig.WriteTo.AzureTableStorage(storage);
            }
            catch
            {
            }

            Log.Logger = logConfig.CreateLogger();
        }
    }
}