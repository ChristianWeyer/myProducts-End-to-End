using System;
using MyProducts.Hosting;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;

namespace MyProducts.Web
{
    public class WebHostStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += DynamicAssemblyResolver.WebAssemblyResolveHandler;

            var startup = new Startup();
            startup.Configuration(app);

            AreaRegistration.RegisterAllAreas();
            BundleConfig.Register(BundleTable.Bundles);
        }
    }
}