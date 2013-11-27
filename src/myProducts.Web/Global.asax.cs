using MyProducts.Web.App_Start;
using System;
using System.Web.Optimization;

namespace MyProducts.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.Register(BundleTable.Bundles);
        }
    }
}