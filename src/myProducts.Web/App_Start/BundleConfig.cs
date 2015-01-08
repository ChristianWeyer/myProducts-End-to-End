using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/libs/css")
               .IncludeDirectory("~/client/libs", "*.css", true)
               .IncludeDirectory("~/client/assets", "*.css", true)
               );

            bundles.Add(new Bundle("~/libs/js")
                .IncludeDirectory("~/client/libs", "*.js", true)
                );

            bundles.Add(new Bundle("~/app/js")
                .IncludeDirectory("~/client/app", "*.js", true)
                .IncludeDirectory("~/client/appServices", "*.js", true)
                .IncludeDirectory("~/client/appStartup", "*.js", true)
            );

            //bundles.Add(new Bundle("~/mobile/mobile/css")
            //   .IncludeDirectory("~/libs/17_Modern", "*.css", true)
            //   .Include("~/libs/50_AngularStuff/loading-bar.css")
            //   .Include("~/mobile/assets/ionic.css")
            //   .Include("~/mobile/assets/mobile.css")
            //   );

            //bundles.Add(new Bundle("~/mobile/js")
            //    .Include("~/mobile/libs/ionic.js")
            //    .Include("~/mobile/libs/ionic-angular.js")
            //    .Include("~/appStartup/mobileApp.js")
            //    );

            //BundleTable.EnableOptimizations = false;
        }
    }
}