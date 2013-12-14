$script(
    [
        "../translations/translations-de",
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

        "services/routeResolverService",
        "services/toastService",
        "services/dataPushService",
        "services/logPushService",
        "services/dialogService",
        "services/articlesApiService",
        "services/personalizationService",
        "services/categoriesService",
        "services/networkStatusService",
        "services/settingsService",

        "controllers/startController",
        "controllers/navigationController",
        "controllers/loginController",
        "controllers/infoController",
        "controllers/settingsController",
        "controllers/statusController"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path("");
        angular.bootstrap(document, ["myApp"]);
    });
});
