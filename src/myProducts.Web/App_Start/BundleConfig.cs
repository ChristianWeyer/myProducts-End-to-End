using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/app/js-lib")
                .Include("~/app/0lib/script.js")
                .Include("~/app/0lib/jquery-*")
                .Include("~/app/0lib/toastr.js")
                .Include("~/app/0lib/spin.js")
                .Include("~/app/0lib/bootstrap*")
                .Include("~/app/0lib/d3.v3.js")
                .Include("~/app/0lib/nv.d3.js")
                .Include("~/app/0lib/angular.js")
                .Include("~/app/0lib/angular-*")
                .Include("~/app/0lib/angularjs*")
                .Include("~/app/0lib/ui-bootstrap*")
                .Include("~/app/0lib/jquery.signalR*")
                .Include("~/app/0lib/log4javascript.js")
                .Include("~/app/0lib/ngStorage.js")
                .Include("~/app/0lib/imageupload.js")
                .Include("~/app/0lib/fastclick.js")
                .Include("~/app/0lib/loading-bar.js")
                .Include("~/cordova.js")
                .Include("~/app/0lib/phonegap-ready.js")
                .Include("~/app/0lib/phonegap-geolocation.js")
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