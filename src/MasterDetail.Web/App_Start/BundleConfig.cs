using System.Web.Optimization;

namespace MasterDetail.Web.App_Start
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/app/js-lib")
                .Include("~/app/js/lib/jquery-2.0.1.js")
                .Include("~/app/js/lib/toastr.js")
                .Include("~/app/js/lib/spin.js")
                .Include("~/app/js/lib/kendo.all.js")
                .Include("~/app/js/lib/bootstrap*")
                .Include("~/app/js/lib/angular*")
                .Include("~/app/js/lib/ui-bootstrap-tpls-0.5.0.js")
                .Include("~/app/js/lib/jquery.signalR-1.1.3.js")
                .Include("~/app/js/lib/lawnchair-0.6.1.js")
                .Include("~/app/js/lib/log4javascript.js")
                .Include("~/app/js/lib/iscroll.js")
                .Include("~/app/js/lib/ng-scrollable.js")
                .Include("~/app/js/lib/ngStorage.js")
                .Include("~/app/js/lib/imageupload.js")
                .Include("~/app/js/lib/fastclick.js")
                .Include("~/app/js/lib/script.js")
                );

            bundles.Add(new ScriptBundle("~/app/app-lib")
                .Include("~/app/translations/translations-de.js")
                .Include("~/app/js/constants.js")

                .Include("~/app/js/infrastructure/tools.js")
                .Include("~/app/js/infrastructure/thinktecture.Authentication.js")
                .Include("~/app/js/infrastructure/thinktecture.SignalR.js")
                .Include("~/app/js/infrastructure/resetSourceWhenDirective.js")
                .Include("~/app/js/infrastructure/chartAutoResizeDirective.js")
                .Include("~/app/js/infrastructure/baseUrlFilter.js")

                .Include("~/app/js/services/routeResolverService.js")
                .Include("~/app/js/services/dataPushService.js")
                .Include("~/app/js/services/logPushService.js")
                .Include("~/app/js/services/dialogService.js")
                .Include("~/app/js/services/articlesApiService.js")
                .Include("~/app/js/services/personalizationService.js")
                .Include("~/app/js/services/networkStatusService.js")
                .Include("~/app/js/services/settingsService.js")

                .Include("~/app/js/controllers/navigationController.js")
                .Include("~/app/js/controllers/loginController.js")
                .Include("~/app/js/controllers/infoController.js")
                .Include("~/app/js/controllers/settingsController.js")
                .Include("~/app/js/controllers/statusController.js")
                );

            bundles.Add(new StyleBundle("~/app/css/common")
                .Include("~/app/css/bootstrap*")
                .Include("~/app/css/toastr.css")
                );
            bundles.Add(new StyleBundle("~/app/css/kendo/css")
                .Include("~/app/css/kendo/kendo.*")
                );
            bundles.Add(new StyleBundle("~/app/css/others")
                .Include("~/app/css/font-awesome.css")
                .Include("~/app/css/angular-carousel.css")
                );
            bundles.Add(new StyleBundle("~/app/css/app")
                .Include("~/app/css/app.css")
                );
        }
    }
}