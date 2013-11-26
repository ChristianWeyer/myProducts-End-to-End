using System.IO;
using System.Reflection;
using System.Web;

namespace MyProducts.Hosting
{
    public class PathHelper
    {
        public static string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath("../" + path);
            }
            else
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"client\" + path);
            }
        }
    }
}
