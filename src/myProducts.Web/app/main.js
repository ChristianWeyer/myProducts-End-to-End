$script.path("app/");

$script(
    [
        "translations/translations-de",
        "constants",
        "../libs/99_appInit/appInit",
        "../appStartup/appStartup"
    ], "app");

$script.ready("app", function () {
    $script(
    [
        "../appServices/tools",
        "../appServices/services/thinktecture.ng.Authentication",
        "../appServices/services/thinktecture.ng.SignalR",
        "../appServices/filters/baseUrlFilter",
        "../appServices/directives/serverValidationDirective",
        "../appServices/services/routeResolverServices",
        "../appServices/services/thinktecture.ng.Toast",
        "../appServices/services/thinktecture.ng.Dialog",
        "../appServices/services/geoLocationTrackerService",

        "services/articlesService",
        "services/articlesPushService",
        "services/categoriesService",
        "services/networkStatusService",
        "services/personalizationService",
        "services/logPushService",
        "services/settingsService",

        "info/Thinktecture.Angular",
        "info/info",

        "login/login",

        "settings/settings",

        "shell/navigation",
        "shell/status",

        "start/start"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path("");
        angular.bootstrap(document, ["myApp"]);
    });
});
