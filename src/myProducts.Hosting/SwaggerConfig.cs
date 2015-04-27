using System;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Application;

namespace MyProducts.Hosting
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.RootUrl(req => req.RequestUri.GetLeftPart(UriPartial.Authority) +
                        req.GetRequestContext().VirtualPathRoot.TrimEnd('/'));
                    c.SingleApiVersion("v1", "myProducts Web API");
                })
                .EnableSwaggerUi();
        }
    }
}