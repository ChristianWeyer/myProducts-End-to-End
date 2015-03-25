using System.Web.Http;
using Swashbuckle.Application;

namespace MyProducts.Hosting
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "myProducts Web API"))
                .EnableSwaggerUi();
        }
    }
}