using Bootstrap;
using Bootstrap.AutoMapper;
using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Web.Http;

namespace MyProducts.Hosting
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            Bootstrapper.With.AutoMapper().Start();
            LoggingConfig.Configure();
            
            SecurityConfig.Register(app);
            
            HttpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(HttpConfiguration);
            app.UseWebApi(HttpConfiguration);

            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
            GlobalHost.Configuration.LongPollDelay = TimeSpan.FromMilliseconds(5000);
            app.MapSignalR();
        }
    }
}