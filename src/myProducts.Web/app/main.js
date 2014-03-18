$script.path("app/");

$script(
    [
        "translations/translations-de",
        "constants",
        "app"
    ], "app");

$script.ready("app", function () {
    $script(
    [
        "infrastructure/tools",
        "infrastructure/thinktecture.ng.Authentication",
        "infrastructure/thinktecture.ng.SignalR",
        "infrastructure/baseUrlFilter",
        "infrastructure/serverValidationDirective",
        "infrastructure/routeResolverService",
        "infrastructure/thinktecture.ng.Toast",
        "infrastructure/thinktecture.ng.Dialog",
        "infrastructure/geoLocationTrackerService",

        "articles/articlesApiService",
        "articles/articlesPushService",
        "articles/categoriesService",

        "info/infoController",

        "login/loginController",

        "log/logPushService",

        "settings/settingsController",
        "settings/settingsService",

        "shell/navigationController",
        "shell/networkStatusService",
        "shell/personalizationService",
        "shell/statusController",

        "start/startController",

         "../mobile/gallery/refreshSlidesDirective",
         "../mobile/login/fixViewportDirective"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path("");
        angular.bootstrap(document, ["myApp"]);
    });
});
