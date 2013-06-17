using System.Web.Routing;
using System;
using System.Web.Http;
using MasterDetail.Web.App_Start;

namespace MasterDetail.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            HubConfig.Register(RouteTable.Routes);
        }
    }
}