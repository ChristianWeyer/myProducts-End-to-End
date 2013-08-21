var myApp = angular.module("myApp", ["ngRoute", "ngTouch", "kendo.directives", "ngSignalR", "tt.Authentication.Providers", "tt.Authentication.Services", "ngCookies", "pascalprecht.translate"]);

myApp.config(["$routeProvider", "$translateProvider", function ($routeProvider, $translateProvider) {
    $routeProvider
        .when("/", { templateUrl: "views/overview.html", controller: "ArticlesController" })
        .when("/details/:id", { templateUrl: "views/details.html", controller: "ArticleDetailsController" })
        .when("/info", { templateUrl: "views/info.html" })
        .when("/login", { templateUrl: "views/login.html", controller: "LoginController" })
        .otherwise({ redirectTo: "/" });
    
    $translateProvider.useStaticFilesLoader({
        prefix: 'data/locale-',
        suffix: '.json'
    });
    $translateProvider.preferredLanguage('de');
    $translateProvider.useLocalStorage();
}]);

myApp.run(["$rootScope", "$location", function ($rootScope, $location) {
    $rootScope.$on("tt:authNRequired", function () {
        $location.path("/login");
    });
    $rootScope.$on("tt:authNConfirmed", function () {
        $location.path("/");
    });
}]);
