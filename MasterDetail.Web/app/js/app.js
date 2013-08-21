var myApp = angular.module("myApp", ["ngRoute", "ngTouch", "ngAnimate", "$strap.directives", "kendo.directives", "ngSignalR", "tt.Authentication.Providers", "tt.Authentication.Services", "ngCookies", "pascalprecht.translate"]);

myApp.config(["$routeProvider", "$translateProvider", "$httpProvider", function ($routeProvider, $translateProvider, $httpProvider) {
    //alert("debug");
    $routeProvider
        .when("/", { templateUrl: "views/overview.html", controller: "ArticlesController" })
        .when("/details/:id", { templateUrl: "views/details.html", controller: "ArticleDetailsController" })
        .when("/info", { templateUrl: "views/info.html" })
        .when("/login", { templateUrl: "views/login.html", controller: "LoginController" })
        .otherwise({ redirectTo: "/" });

    $translateProvider.translations("de", tt.translations.de);
    $translateProvider.useStaticFilesLoader({
        prefix: 'translations/locale-',
        suffix: '.json'
    });
    $translateProvider.preferredLanguage('de');
    $translateProvider.useLocalStorage();

    $httpProvider.responseInterceptors.push("loadingIndicatorInterceptor");
    var transformRequest = function (data) {
        var sp = document.getElementById("spinner");
        theSpinner.spin(sp);

        return data;
    };
    $httpProvider.defaults.transformRequest.push(transformRequest);
}]);

myApp.run(["$http", "$templateCache", "$rootScope", "$location", function ($http, $templateCache, $rootScope, $location) {
    $http.get("views/overview.html", { cache: $templateCache });
    $http.get("views/details.html", { cache: $templateCache });
    $http.get("views/info.html", { cache: $templateCache });
    $http.get("views/login.html", { cache: $templateCache });

    var oldOpen = XMLHttpRequest.prototype.open;
    XMLHttpRequest.prototype.open = function (method, url, async, user, pass) {
        if (url.indexOf("/signalr") === -1) {
            this.addEventListener("readystatechange", function () {
                if (this.readyState === 1) {
                    theSpinner.spin(document.getElementById("spinner"));
                };
                if (this.readyState === 4) {
                    theSpinner.stop();
                };
            }, false);    
        }
        
        oldOpen.call(this, method, url, async, user, pass); 
    };

    $rootScope.$on("$locationChangeStart", function () {
        if (!$rootScope.ttUserAuthenticated) {
            $rootScope.$broadcast(tt.authentication.constants.authenticationRequired);
        }
    });

    $rootScope.$on(tt.authentication.constants.authenticationRequired, function () {
        $location.path("/login");
    });
    $rootScope.$on(tt.authentication.constants.authenticationConfirmed, function () {
        $rootScope.ttUserAuthenticated = true;
        $location.path("/");
    });
}]);

myApp.factory("loadingIndicatorInterceptor", function ($q) {
    return function (promise) {
        return promise.then(
            function (response) {
                theSpinner.stop();

                return response;
            },
            function (response) {
                theSpinner.stop();

                return $q.reject(response);
            });
    };
});

var theSpinner = new Spinner({
    lines: 12,
    length: 20,
    width: 2,
    radius: 10,
    color: '#000',
    speed: 1,
    trail: 100,
    shadow: true,
    top: "auto",
    left: "auto"
});