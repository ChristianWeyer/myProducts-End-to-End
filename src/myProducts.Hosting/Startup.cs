using Microsoft.AspNet.SignalR;
using MyProducts.Services;
using Owin;
using System;
using System.Web.Http;

namespace MyProducts.Hosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LoggingConfig.Configure();
            DataMapper.Configure();

            var webApiConfig = new HttpConfiguration();
            WebApiConfig.Register(webApiConfig);
            SecurityConfig.Register(webApiConfig);
            app.UseWebApi(webApiConfig);

            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(1);
            GlobalHost.Configuration.LongPollDelay = TimeSpan.FromMilliseconds(5000);

            app.MapSignalR();
        }
    }
}