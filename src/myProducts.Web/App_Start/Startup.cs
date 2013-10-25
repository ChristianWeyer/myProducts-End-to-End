using Microsoft.AspNet.SignalR;
using Owin;
using System;

namespace MyProducts.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
            GlobalHost.Configuration.LongPollDelay = TimeSpan.FromMilliseconds(5000);

            builder.MapSignalR();
        }
    }
}