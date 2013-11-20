using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace MyProducts.SelfHost
{
    partial class MyProductsService : ServiceBase
    {
        private static IDisposable _webApiServer;

        public MyProductsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _webApiServer = WebApp.Start<WebApiStartup>("");
        }

        protected override void OnStop()
        {
            if (_webApiServer != null)
            {
                _webApiServer.Dispose();
            }
        }

        public void InteractiveStart(string[] args)
        {
            OnStart(args);
        }

        public void InteractiveStop()
        {
            OnStop();
        }
    }
}
