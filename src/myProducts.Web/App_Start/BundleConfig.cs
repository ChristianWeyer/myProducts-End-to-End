using System.Web.Optimization;

namespace MyProducts.Web
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/app/js-lib")
                .Include("~/app/js/lib/script.js")
                .Include("~/app/js/lib/jquery-2.0.1.js")
                .Include("~/app/js/lib/toastr.js")
                .Include("~/app/js/lib/spin.js")
                .Include("~/app/js/lib/bootstrap*")
                .Include("~/app/js/lib/d3.v3.js")
                .Include("~/app/js/lib/nv.d3.js")
                .Include("~/app/js/lib/angular.js")
                .Include("~/app/js/lib/angular-*")
                .Include("~/app/js/lib/angularjs*")
                .Include("~/app/js/lib/ui-bootstrap*")
                .Include("~/app/js/lib/jquery.signalR*")
                .Include("~/app/js/lib/log4javascript.js")
                .Include("~/app/js/lib/ngStorage.js")
                .Include("~/app/js/lib/imageupload.js")
                .Include("~/app/js/lib/fastclick.js")
                .Include("~/app/js/lib/loading-bar.js")
                .Include("~/app/js/lib/phonegap-ready.js")
                .Include("~/app/js/lib/phonegap-geolocation.js")
            );

            bundles.Add(new StyleBundle("~/app/css/common")
                .Include("~/app/css/modern.css")
                .Include("~/app/css/modern-responsive.css")
                .Include("~/app/css/bootstrap.css")
                .Include("~/app/css/tweak-bootstrap.css")
                .Include("~/app/css/bootstrap-responsive.css")
                .Include("~/app/css/bootstrap-switch.css")
                .Include("~/app/css/toastr.css")
                .Include("~/app/css/nv.d3.css")
                .Include("~/app/css/font-awesome.css")
                .Include("~/app/css/angular-carousel.css")
                .Include("~/app/css/loading-bar.css")
                );
            bundles.Add(new StyleBundle("~/app/css/app")
                .Include("~/app/css/app.css")
                .Include("~/app/css/loadspinner.css")
                );
        }
    }
}