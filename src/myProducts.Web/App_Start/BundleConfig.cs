using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/libs/css")
               .IncludeDirectory("~/libs", "*.css", true)
               .IncludeDirectory("~/assets", "*.css", true)
               );

            bundles.Add(new Bundle("~/libs/js")
                .IncludeDirectory("~/libs", "*.js", true)
                );

            bundles.Add(new Bundle("~/app/js")
                .IncludeDirectory("~/app", "*.js", true)
                .IncludeDirectory("~/appServices", "*.js", true)
                .IncludeDirectory("~/appStartup", "*.js", true)
            );

            bundles.Add(new Bundle("~/mobile/mobile/css")
               .IncludeDirectory("~/libs/17_Modern", "*.css", true)
               .Include("~/libs/50_AngularStuff/loading-bar.css")
               .Include("~/mobile/assets/ionic.css")
               .Include("~/mobile/assets/mobile.css")
               );

            bundles.Add(new Bundle("~/mobile/js")
                .Include("~/mobile/libs/ionic.js")
                .Include("~/mobile/libs/ionic-angular.js")
                .Include("~/appStartup/mobileApp.js")
                );

            //BundleTable.EnableOptimizations = false;
        }
    }
}