var myApp = angular.module("myApp", ["ngRoute", "ngTouch", "$strap.directives", "kendo.directives", "ngSignalR", "tt.Authentication.Providers", "tt.Authentication.Services", "ngCookies", "pascalprecht.translate"]);

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

myApp.run(["$http", "$templateCache", "$rootScope", "$location", function ($http, $templateCache, $rootScope, $location) {
    $http.get("views/overview.html", { cache: $templateCache });
    $http.get("views/details.html", { cache: $templateCache });
    $http.get("views/info.html", { cache: $templateCache });
    $http.get("views/login.html", { cache: $templateCache });
    
    $rootScope.$on(tt.authentication.constants.authenticationRequired, function () {
        $location.path("/login");
    });
    $rootScope.$on(tt.authentication.constants.authenticationConfirmed, function () {
        $location.path("/");
    });
}]);
