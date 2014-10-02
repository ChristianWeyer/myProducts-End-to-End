using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;

namespace MyProducts.Hosting
{
    static internal class IocConfiguration
    {
        internal static AutofacWebApiDependencyResolver ConfigureContainerForWebApi()
        {
            var builder = new ContainerBuilder();

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            builder.RegisterApiControllers(Directory.GetFiles(path, "*Services.dll").Select(Assembly.LoadFrom).ToArray());

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }
    }
}