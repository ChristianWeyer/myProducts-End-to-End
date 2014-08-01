using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.IgnoreList.Ignore("app.js");

            bundles.Add(new Bundle("~/app/app/css")
               .IncludeDirectory("~/libs", "*.css", true)
               );

            bundles.Add(new Bundle("~/app/js")
                .IncludeDirectory("~/libs", "*.js", true)
                );

            bundles.Add(new Bundle("~/mobile/mobile/css")
               .IncludeDirectory("~/libs/17_Modern", "*.css", true)
               .Include("~/mobile/50_AngularStuff/loading-bar.css")
               .Include("~/mobile/assets/ionic.css")
               .Include("~/mobile/assets/mobile.css")
               );

            bundles.Add(new Bundle("~/mobile/js")
                .Include("~/mobile/libs/ionic.js")
                .Include("~/mobile/libs/ionic-angular.js")
                .Include("~/mobile/mobile.js")
                );
        }
    }
}