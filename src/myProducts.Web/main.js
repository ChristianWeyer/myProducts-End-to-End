$script.path("app/js/");

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
        "infrastructure/thinktecture.Authentication",
        "infrastructure/thinktecture.SignalR",
        "infrastructure/resetSourceWhenDirective",
        "infrastructure/chartAutoResizeDirective",
        "infrastructure/baseUrlFilter",

        "services/routeResolverService",
        "services/toastService",
        "services/dataPushService",
        "services/logPushService",
        "services/dialogService",
        "services/articlesApiService",
        "services/personalizationService",
        "services/networkStatusService",
        "services/settingsService",

        "controllers/navigationController",
        "controllers/loginController",
        "controllers/infoController",
        "controllers/settingsController",
        "controllers/statusController"
    ], "bundle");

    $script.ready("bundle", function () {
        $script.path('');
        angular.bootstrap(document, ['myApp']);
    });
});
