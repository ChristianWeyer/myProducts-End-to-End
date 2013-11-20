using MyProducts.Hosting;
using MyProducts.Services;
using Owin;
using System.Web.Http;

namespace MyProducts.SelfHost
{
    public class SelfHostStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseFileServer(opts =>
            {
                opts.WithRequestPath("/app");
                opts.WithPhysicalPath(@"..\myProducts.Web\app");
            });

            LoggingConfig.Configure();
            DataMapper.Configure();

            var webApiConfig = new HttpConfiguration();
            WebApiConfig.Register(webApiConfig);
            SecurityConfig.Register(webApiConfig);
            builder.UseWebApi(webApiConfig);

            // TODO: load assemblies with hubs...
            builder.MapSignalR();
        }
    }
}