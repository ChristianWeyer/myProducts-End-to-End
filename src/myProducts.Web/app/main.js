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
        "_infrastructure/tools",
        "_infrastructure/thinktecture.ng.Authentication",
        "_infrastructure/thinktecture.ng.SignalR",
        "_infrastructure/baseUrlFilter",
        "_infrastructure/serverValidationDirective",
        "_infrastructure/routeResolverService",
        "_infrastructure/toastService",
        "_infrastructure/dialogService",
        "_infrastructure/geoLocationTrackerService",

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

        "start/startController"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path("");
        angular.bootstrap(document, ["myApp"]);
    });
});
