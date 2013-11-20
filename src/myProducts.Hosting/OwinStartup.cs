using Microsoft.AspNet.SignalR;
using Owin;
using System;

namespace MyProducts.Hosting
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
            GlobalHost.Configuration.LongPollDelay = TimeSpan.FromMilliseconds(5000);

            builder.MapSignalR();
        }
    }
}