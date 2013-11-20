using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace MyProducts.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new MyProductsService();

            if (args.Length == 0)
            {
                ServiceBase.Run(service);
            }
            else
            {
                var arg = args[0];

                try
                {
                    switch (arg)
                    {
                        case "--console":
                            RunInteractive(service, args);
                            break;
                        case "--install":
                            ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                            break;
                        case "--uninstall":
                            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // TODO: logging...
                    throw;
                }
            }
        }

        private static void RunInteractive(MyProductsService service, string[] args)
        {
            service.InteractiveStart(args);

            Console.WriteLine("SQLnSAP Management Service läuft...");
            Console.WriteLine("Press Enter to Quit...");
            Console.ReadLine();

            service.InteractiveStop();
        }
    }
}
