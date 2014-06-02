using MyProducts.Hosting;
using Swashbuckle.Application;

namespace MyProducts.Web
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(Startup.HttpConfiguration);

            // NOTE: If you want to customize the generated swagger or UI, use SwaggerSpecConfig and/or SwaggerUiConfig here ...
        }
    }
}