using System;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Transports;

namespace MasterDetail.Web.App_Start
{
    public class HubConfig
    {
        public static void Register(RouteCollection routes)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
            LongPollingTransport.LongPollDelay = 5000;
            
            routes.MapHubs();
        }
    }
}