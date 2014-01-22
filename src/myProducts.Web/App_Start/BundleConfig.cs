using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/app/js-lib")
                .Include("~/app/_lib/script.js")
                .Include("~/app/_lib/jquery-2.0.1.js")
                .Include("~/app/_lib/toastr.js")
                .Include("~/app/_lib/spin.js")
                .Include("~/app/_lib/bootstrap*")
                .Include("~/app/_lib/d3.v3.js")
                .Include("~/app/_lib/nv.d3.js")
                .Include("~/app/_lib/angular.js")
                .Include("~/app/_lib/angular-*")
                .Include("~/app/_lib/angularjs*")
                .Include("~/app/_lib/ui-bootstrap*")
                .Include("~/app/_lib/jquery.signalR*")
                .Include("~/app/_lib/log4javascript.js")
                .Include("~/app/_lib/ngStorage.js")
                .Include("~/app/_lib/imageupload.js")
                .Include("~/app/_lib/fastclick.js")
                .Include("~/app/_lib/loading-bar.js")
                .Include("~/cordova.js")
                .Include("~/app/_lib/phonegap-ready.js")
                .Include("~/app/_lib/phonegap-geolocation.js")
            );

            bundles.Add(new StyleBundle("~/app/css/common")
                .Include("~/app/content/css/modern.css")
                .Include("~/app/content/css/modern-responsive.css")
                .Include("~/app/content/css/bootstrap.css")
                .Include("~/app/content/css/tweak-bootstrap.css")
                .Include("~/app/content/css/bootstrap-responsive.css")
                .Include("~/app/content/css/bootstrap-switch.css")
                .Include("~/app/content/css/toastr.css")
                .Include("~/app/content/css/nv.d3.css")
                .Include("~/app/content/css/font-awesome.css")
                .Include("~/app/content/css/angular-carousel.css")
                .Include("~/app/content/css/loading-bar.css")
                );
            bundles.Add(new StyleBundle("~/app/css/app")
                .Include("~/app/content/css/app.css")
                .Include("~/app/content/css/app-responsive.css")
                .Include("~/app/content/css/loadspinner.css")
                );
        }
    }
}