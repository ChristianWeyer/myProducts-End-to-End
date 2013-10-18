using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.Web.Http;
using MyProducts.Web.App_Start;

namespace MyProducts.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            BundleConfig.Register(BundleTable.Bundles);
        }
    }
}