var myApp = angular.module("myApp", ["kendo", "ngSignalR", "authenticationInterceptor"]);

myApp.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/", { templateUrl: "views/overview.html", controller: "ArticlesController" })
        .when("/details/:id", { templateUrl: "views/details.html", controller: "ArticleDetailsController" })
        .when("/info", { templateUrl: "views/info.html" })
        .when("/login", { templateUrl: "views/login.html", controller: "LoginController" })
        .otherwise({ redirectTo: "/" });
}]);

myApp.run(["$rootScope", "$location", function ($rootScope, $location) {
    $rootScope.$on("tt:authNRequired", function () {
        $location.path("/login");
    });
    $rootScope.$on("tt:authNConfirmed", function () {
        $location.path("/");
    });
}]);
