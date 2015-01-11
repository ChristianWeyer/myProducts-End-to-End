using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/client/libs/css")
               .IncludeDirectory("~/client/libs", "*.css", true)
               .IncludeDirectory("~/client/assets", "*.css", true)
               );

            bundles.Add(new Bundle("~/client/libs/js")
                .IncludeDirectory("~/client/libs", "*.js", true)
                );

            bundles.Add(new Bundle("~/client/app/js")
                .IncludeDirectory("~/client/app", "*.js", true)
                .IncludeDirectory("~/client/appServices", "*.js", true)
                .IncludeDirectory("~/client/appStartup", "*.js", true)
            );

            //BundleTable.EnableOptimizations = true;
        }
    }
}