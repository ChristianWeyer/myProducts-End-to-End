$script.path("app/");

$script(
    [
        "translations/translations-de",
        "constants",
        "../libs/99_appInit/20/app"
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

        "info/info",

        "login/login",

        "log/logPushService",

        "settings/settings",
        "settings/settingsService",

        "shell/navigation",
        "shell/networkStatusService",
        "shell/personalizationService",
        "shell/status",

        "start/start",

         "../mobile/gallery/refreshSlidesDirective",
         "../mobile/login/fixViewportDirective"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path("");
        angular.bootstrap(document, ["myApp"]);
    });
});
