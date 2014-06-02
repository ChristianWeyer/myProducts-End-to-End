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
            var startup = new Startup();
            startup.Configuration(app);

            AreaRegistration.RegisterAllAreas();
            BundleConfig.Register(BundleTable.Bundles);
            SwaggerConfig.Register();
        }
    }
}