using Topshelf;

namespace MyProducts.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
