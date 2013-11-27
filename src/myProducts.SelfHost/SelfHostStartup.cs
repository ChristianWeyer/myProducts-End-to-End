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
                opts.WithRequestPath("");
                opts.WithPhysicalPath("client");
                opts.WithDefaultFileNames("index.html");
            });
            builder.UseFileServer(opts =>
            {
                opts.WithRequestPath("/images");
                opts.WithPhysicalPath("images");
            });

            LoggingConfig.Configure();
            DataMapper.Configure();

            var webApiConfig = new HttpConfiguration();
            WebApiConfig.Register(webApiConfig);
            SecurityConfig.Register(webApiConfig);
            builder.UseWebApi(webApiConfig);

            builder.MapSignalR();
        }
    }
}