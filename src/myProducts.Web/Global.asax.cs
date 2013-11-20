using MyProducts.Hosting;
using MyProducts.Services;
using MyProducts.Web.App_Start;
using System;
using System.Web.Http;
using System.Web.Optimization;

namespace MyProducts.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            LoggingConfig.Configure();
            DataMapper.Configure(); 
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.Register(BundleTable.Bundles);
        }
    }
}