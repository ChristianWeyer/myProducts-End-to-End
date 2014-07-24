using System.Threading.Tasks;
using Microsoft.Owin;
using MyProducts.Hosting;

namespace MyProducts.Hosting
{
    public class FakeHeaderMiddleware : OwinMiddleware
    {
        public FakeHeaderMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.Response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;
                resp.Headers.Add("X-TT-Fake", new[] { "foo" });
            }, context.Response);

            await Next.Invoke(context);
        }
    }
}

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseFakeHeader(this IAppBuilder app)
        {
            app.Use(typeof(FakeHeaderMiddleware));
            return app;
        }
    }
}