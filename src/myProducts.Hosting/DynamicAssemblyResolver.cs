using System;
using System.IO;
using System.Reflection;

namespace MyProducts.Hosting
{
    public static class DynamicAssemblyResolver
    {
        public static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs e)
        {
            try
            {
                string[] assemblyDetail = e.Name.Split(',');
                string assemblyBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Assembly assembly = Assembly.LoadFrom(assemblyBasePath + @"\" + assemblyDetail[0] + ".dll");

                return assembly;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed resolving assembly: ", ex);
            }
        }
    }
}
