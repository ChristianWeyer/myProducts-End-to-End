using Owin;
using System.Web.Http;

namespace MyProducts.SelfHost
{
    public class WebApiStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseFileServer(opts =>
            {
                opts.WithRequestPath("/app");
                opts.WithPhysicalPath("app");
            });

            builder.UseWebApi(new HttpConfiguration());
        }
    }
}