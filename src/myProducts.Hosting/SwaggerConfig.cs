using System.Web.Http;

namespace MyProducts.Hosting
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Swashbuckle.Bootstrapper.Init(config);

            // NOTE: If you want to customize the generated swagger or UI, use SwaggerSpecConfig and/or SwaggerUiConfig here ...
        }
    }
}