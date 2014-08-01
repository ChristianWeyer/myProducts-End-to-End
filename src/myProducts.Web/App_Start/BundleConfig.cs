using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.IgnoreList.Ignore("app.js");

            bundles.Add(new Bundle("~/app/css")
               .IncludeDirectory("~/libs", "*.css", true)
               );

            bundles.Add(new Bundle("~/app/js")
                .IncludeDirectory("~/libs", "*.js", true)
                );

            bundles.Add(new Bundle("~/mobile/css")
               .Include("~/mobile/content/css/ionic.css")
               .Include("~/mobile/content/css/mobile.css")
               );

            bundles.Add(new Bundle("~/mobile/js")
                .Include("~/mobile/libs/ionic.js")
                .Include("~/mobile/libs/ionic-angular.js")
                .Include("~/mobile/mobile.js")
                );
        }
    }
}