var myApp = angular.module("myApp", ["ngRoute", "ngTouch", "ngAnimate", "$strap.directives", "ui.bootstrap", "kendo.directives", "ngSignalR", "tt.Authentication", "ngCookies", "pascalprecht.translate"]);

myApp.config(["$routeProvider", "$translateProvider", "$httpProvider", function ($routeProvider, $translateProvider, $httpProvider) {
    ttTools.initLogger(ttTools.baseUrl + "api/log");
    ttTools.logger.info("Configuring myApp...");
    
    $routeProvider
        .when("/", { templateUrl: "app/views/overview.html", controller: "ArticlesController" })
        .when("/details/:id", { templateUrl: "app/views/details.html", controller: "ArticleDetailsController" })
        .when("/info", { templateUrl: "app/views/info.html" })
        .when("/log", { templateUrl: "app/views/log.html", controller: "LogController" })
        .when("/login", { templateUrl: "app/views/login.html", controller: "LoginController" })
        .otherwise({ redirectTo: "/" });

    $translateProvider.translations("de", tt.translations.de);
    $translateProvider.useStaticFilesLoader({
        prefix: "app/translations/locale-",
        suffix: ".json"
    });
    $translateProvider.preferredLanguage("de");
    $translateProvider.useLocalStorage();

    $httpProvider.responseInterceptors.push("loadingIndicatorInterceptor");
    var transformRequest = function (data) {
        var sp = document.getElementById("spinner");
        theSpinner.spin(sp);

        return data;
    };
    $httpProvider.defaults.transformRequest.push(transformRequest);
}]);

myApp.run(["$http", "$templateCache", "$rootScope", "$location", "$translate", "alertService", "dialogService", function ($http, $templateCache, $rootScope, $location, $translate, alertService, dialogService) {
    window.applicationCache.addEventListener('updateready', function (ev) {
        if (window.applicationCache.status == window.applicationCache.UPDATEREADY) {
            console.log('CACHE: Browser downloaded a new app cache manifest.');
            window.applicationCache.swapCache();

            $rootScope.$apply(dialogService.showModalDialog({}, {
                headerText: 'App Update',
                bodyText: $translate("APP_UPDATE_BODY"),
                closeButtonText: $translate("COMMON_NO"),
                actionButtonText: $translate("COMMON_YES"),
                callback: function () {
                    window.location.reload();
                    console.log('CACHE: App will be updated...');
                }
            }));
        } else {
            console.log('CACHE: Manifest didn\'t change.');
        }
    }, false);
    
    $http.get("app/views/details.html", { cache: $templateCache });
    $http.get("app/views/info.html", { cache: $templateCache });
    $http.get("app/views/login.html", { cache: $templateCache });
    $http.get("app/views/overview.html", { cache: $templateCache });
    
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
        if (!$rootScope.tt.authentication.userLoggedIn) {
            $rootScope.$broadcast(tt.authentication.constants.authenticationRequired);
        }
    });

    $rootScope.$on(tt.authentication.constants.authenticationRequired, function () {
        $location.path("/login");
    });
    $rootScope.$on(tt.authentication.constants.loginConfirmed, function () {
        $location.path("/");
    });
    $rootScope.$on(tt.authentication.constants.loginFailed, function () {
        $location.path("/login");
        alertService.pop({
            title: "Login", body: $translate("LOGIN_FAILED"), type: "error"
        });
    });
    $rootScope.$on(tt.authentication.constants.logoutConfirmed, function () {
        $location.path("/login");
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