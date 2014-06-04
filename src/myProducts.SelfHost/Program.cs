using System;
using MyProducts.Hosting;
using Topshelf;

namespace MyProducts.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += DynamicAssemblyResolver.AssemblyResolveHandler;

            HostFactory.Run(x =>
            {
                x.Service<MyProductsHost>(s =>
                {
                    s.ConstructUsing(name => new MyProductsHost());
                    s.WhenStarted(tc => tc.OnStart());
                    s.WhenStopped(tc => tc.OnStop());
                });
                x.RunAsNetworkService();

                x.SetDescription("Thinktecture myProducts Server");
                x.SetDisplayName("myProducts Server");
                x.SetServiceName("myProductsServer");
            });

            Console.ReadLine();
        }   
    }
}
