if (ttMobile) {
    angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "ngStorage", "nvd3", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
} else {
    angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "$strap.directives", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "nvd3", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
}

angular.module("myApp").config(["$urlRouterProvider", "$stateProvider", "$locationProvider", "$translateProvider", "$httpProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider", "$provide", "cfpLoadingBarProvider", "tokenAuthenticationServiceProvider",
    function ($urlRouterProvider, $stateProvider, $locationProvider, $translateProvider, $httpProvider, routeResolverProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);

        cfpLoadingBarProvider.includeSpinner = false;

        tokenAuthenticationServiceProvider.setUrl(ttTools.baseUrl + "token");

        ttTools.initLogger(ttTools.baseUrl + "api/log");
        ttTools.logger.info("Configuring myApp...");

        angular.module("myApp").lazy =
        {
            controller: $controllerProvider.register,
            directive: $compileProvider.directive,
            filter: $filterProvider.register,
            factory: $provide.factory,
            service: $provide.service
        };

        var viewBaseUrl = "app/";

        if (ttMobile) {
            viewBaseUrl = "mobile/";
        }

        routeResolverProvider.routeConfig.setBaseDirectories(viewBaseUrl, "app/");

        $urlRouterProvider.otherwise("/");

        $stateProvider
          .state("info", {
              url: "/info",
              templateUrl: viewBaseUrl + "info/info.html",
              controller: "infoController"
          })
          .state("settings", {
              url: "/settings",
              templateUrl: viewBaseUrl + "settings/settings.html",
              controller: "settingsController"
          })
          .state("login", {
              url: "/login",
              templateUrl: viewBaseUrl + "login/login.html",
              controller: "loginController"
          })
          .state("start", {
              url: "/",
              templateUrl: viewBaseUrl + "start/start.html",
              controller: "startController"
          });

        $provide.factory("$stateProviderService", function () {
            return $stateProvider;
        });

        $translateProvider.translations("de", tt.translations.de);
        $translateProvider.useStaticFilesLoader({
            prefix: "app/translations/locale-",
            suffix: ".json"
        });
        $translateProvider.preferredLanguage("en");
        $translateProvider.useLocalStorage();
    }]);

angular.module("myApp").run(["$localStorage", "$stateProviderService", "$state", "$http", "$templateCache", "$rootScope", "$location", "$translate", "toastService", "dialogService", "routeResolver", "personalizationService", "categoriesService", "geoLocationTracker", "articlesPushService", "logPushService",
    function ($localStorage, $stateProviderService, $state, $http, $templateCache, $rootScope, $location, $translate, toast, dialog, routeResolver, personalization, categories, geoLocationTracker, articlesPush, logPush) {
        geoLocationTracker.startSendPosition(10000, function (pos) { });

        window.addEventListener("online", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, true));
        }, true);
        window.addEventListener("offline", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, false));
        }, true);

        $http.defaults.headers.common["Accept-Language"] = $translate.proposedLanguage();
        $rootScope.$on("$translateChangeSuccess", function () {
            $http.defaults.headers.common["Accept-Language"] = $translate.proposedLanguage();
        });

        var currentPath = $location.path();
        var viewsDir = routeResolver.routeConfig.getViewsDirectory();
        $http.get(viewsDir + "info/info.html", { cache: $templateCache });

        $rootScope.$on(tt.authentication.loggedIn, function () {
            $http({ method: "GET", url: ttTools.baseUrl + "api/personalization" })
            .success(function (data) {
                if (!categories.data) {
                    $http({ method: "GET", url: ttTools.baseUrl + "api/categories" })
                    .success(function (data) {
                        categories.data = data;
                    });
                }

                personalization.data = data;
                var route = routeResolver.route;

                angular.forEach(data.Features, function (value, key) {
                    // TODO: check how to add states only when not yet in $state - this is too dirty
                    try {
                        $stateProviderService.state(value.Module.toLowerCase(), route.resolve(value));
                        $http.get(viewsDir + value.Module.toLowerCase() + "/" + value.Module.toLowerCase() + ".html", { cache: $templateCache });
                    } catch (e) {
                    }
                });

                $rootScope.$broadcast(tt.personalization.dataLoaded);
                $location.path(currentPath);
            });
        });

        // TODO: what about unloading!?

        $rootScope.$on(tt.authentication.authenticationRequired, function () {
            $location.path("/login");
        });
        $rootScope.$on(tt.authentication.loginConfirmed, function () {
            $location.path("/");

            toast.pop({
                title: "Login",
                body: $translate.instant("LOGIN_SUCCESS"),
                type: "success"
            });
        });
        $rootScope.$on(tt.authentication.loginFailed, function () {
            $location.path("/login");
            toast.pop({
                title: "Login",
                body: $translate.instant("LOGIN_FAILED"),
                type: "error"
            });
        });
        $rootScope.$on(tt.authentication.logoutConfirmed, function () {
            $localStorage.$reset();
            $location.path("/login");
        });

        $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
            if (!$rootScope.tt.authentication.userLoggedIn) {
                $rootScope.$broadcast(tt.authentication.authenticationRequired);
            }
        });

        $rootScope.ttAppLoaded = true;
    }]);

angular.module("myApp").animation(".reveal-animation", function () {
    return {
        enter: function (element, done) {
            element.css("display", "none");
            element.fadeIn(500, done);
            return function () {
                element.stop();
            };
        },
        leave: function (element, done) {
            element.fadeOut(500, done);
            return function () {
                element.stop();
            };
        }
    };
});
