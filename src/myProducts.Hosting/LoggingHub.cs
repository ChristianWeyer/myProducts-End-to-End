using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyProducts.Hosting
{
    [HubName("logHub")]
    public class LoggingHub : Hub
    {
    }
}