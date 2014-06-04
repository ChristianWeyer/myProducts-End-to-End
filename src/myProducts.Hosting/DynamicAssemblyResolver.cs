using System;
using System.IO;
using System.Reflection;

namespace MyProducts.Hosting
{
    public static class DynamicAssemblyResolver
    {
        public static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs e)
        {
            var assemblyDetail = e.Name.Split(',');

            return ResolveHandler(assemblyDetail[0], String.Empty);
        }

        public static Assembly WebAssemblyResolveHandler(object sender, ResolveEventArgs e)
        {
            var assemblyDetail = e.Name.Split(',');

            return ResolveHandler(assemblyDetail[0], "bin");
        }

        private static Assembly ResolveHandler(string assemblyName, string path)
        {            
            var assemblyBasePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            try
            {
                var assembly = Assembly.LoadFrom(Path.Combine(Path.Combine(assemblyBasePath, path), assemblyName + ".dll"));

                return assembly;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed resolving assembly: ", ex);
            }
        }
    }
}
