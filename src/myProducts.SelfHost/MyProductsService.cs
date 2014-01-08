using Microsoft.Owin.Hosting;
using MyProducts.Hosting;
using MyProducts.SelfHost.Properties;
using System;

namespace MyProducts.SelfHost
{
    partial class MyProductsService
    {
        private static IDisposable server;

        public void OnStart()
        {
            server = WebApp.Start<SelfHostStartup>(Settings.Default.BaseUrl);
        }

        public void OnStop()
        {
            if (server != null)
            {
                server.Dispose();
            }
        }
    }
}
