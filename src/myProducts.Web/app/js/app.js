var app = angular.module("myApp", ["ngRoute", "ngTouch", "ngAnimate", "$strap.directives", "ui.bootstrap", "tt.SignalR", "tt.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "nvd3ChartDirectives", "jmdobry.angular-cache", "ionic", "chieffancypants.loadingBar"]);

app.config(["$routeProvider", "$locationProvider", "$translateProvider", "$httpProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider", "$provide", "cfpLoadingBarProvider", "tokenAuthenticationProvider",
    function ($routeProvider, $locationProvider, $translateProvider, $httpProvider, routeResolverProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, cfpLoadingBarProvider, tokenAuthenticationProvider) {
        cfpLoadingBarProvider.includeSpinner = false;

        //tokenAuthenticationProvider.setStorage("private");
        tokenAuthenticationProvider.setUrl("api/controller/token");

        var mobile = window.location.pathname.indexOf("mobile/") > -1;

        ttTools.initLogger(ttTools.baseUrl + "api/log");
        ttTools.logger.info("Configuring myApp...");

        app.lazy =
        {
            controller: $controllerProvider.register,
            directive: $compileProvider.directive,
            filter: $filterProvider.register,
            factory: $provide.factory,
            service: $provide.service
        };

        var routeBaseUrl = "app/";
        var relativePath = "";

        if (mobile) {
            routeBaseUrl = "";
            relativePath = "../";
        }

        routeResolverProvider.routeConfig.setBaseDirectories(routeBaseUrl + "views/", relativePath + "app/js/controllers/");

        $routeProvider
            .when("/", { templateUrl: routeBaseUrl + "views/start.html", controller: "StartController" })
            .when("/info", { templateUrl: routeBaseUrl + "views/info.html", controller: "InfoController" })
            .when("/settings", { templateUrl: routeBaseUrl + "views/settings.html", controller: "SettingsController" })
            .when("/login", { templateUrl: routeBaseUrl + "views/login.html", controller: "LoginController" });

        $provide.factory("$routeProviderService", function () {
            return $routeProvider;
        });

        $provide.factory("routeResolverProviderService", function () {
            return routeResolverProvider;
        });

        $translateProvider.translations("de", tt.translations.de);
        $translateProvider.useStaticFilesLoader({
            prefix: relativePath + "app/translations/locale-",
            suffix: ".json"
        });
        $translateProvider.preferredLanguage("en");
        $translateProvider.useLocalStorage();
    }]);

app.run(["$http", "$templateCache", "$rootScope", "$location", "$translate", "toast", "dialog", "$route", "$routeProviderService", "routeResolverProviderService", "personalization", "categories",
    function ($http, $templateCache, $rootScope, $location, $translate, toast, dialog, $route, $routeProviderService, routeResolverProviderService, personalization, categories) {

        window.addEventListener("online", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, true));
        }, true);
        window.addEventListener("offline", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, false));
        }, true);

        $http.defaults.headers.common["Accept-Language"] = $translate.uses();
        $rootScope.$on("$translateChangeSuccess", function () {
            $http.defaults.headers.common["Accept-Language"] = $translate.uses();
        });

        var viewsDir = routeResolverProviderService.routeConfig.getViewsDirectory();
        $http.get(viewsDir + "info.html", { cache: $templateCache });

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
                var route = routeResolverProviderService.route;

                angular.forEach(data.Features, function (value, key) {
                    $routeProviderService.when(value.Url, route.resolve(value.Module));
                });

                $rootScope.$broadcast(tt.personalization.dataLoaded);
                $route.reload();
            });
        });

        // TODO: what about unloading!?

        window.applicationCache.addEventListener("updateready", function (ev) {
            if (window.applicationCache.status == window.applicationCache.UPDATEREADY) {
                console.log("CACHE: Browser downloaded a new app cache manifest.");
                window.applicationCache.swapCache();

                $rootScope.$apply(dialog.showModalDialog({}, {
                    headerText: "App Update",
                    bodyText: $translate("APP_UPDATE_BODY"),
                    closeButtonText: $translate("COMMON_NO"),
                    actionButtonText: $translate("COMMON_YES")
                }).then(function (result) {
                    window.location.reload();
                    console.log("CACHE: App will be updated...");
                }));
            } else {
                console.log("CACHE: Manifest didn\'t change.");
            }
        }, false);

        $rootScope.$on("$routeChangeStart", function () {
            if (!$rootScope.tt.authentication.userLoggedIn) {
                $rootScope.$broadcast(tt.authentication.authenticationRequired);
            }
        });
        $rootScope.$on(tt.authentication.authenticationRequired, function () {
            $location.path("/login");
        });
        $rootScope.$on(tt.authentication.loginConfirmed, function () {
            $location.path("/");

            toast.pop({
                title: "Login",
                body: $translate("LOGIN_SUCCESS"),
                type: "success"
            });
        });
        $rootScope.$on(tt.authentication.loginFailed, function () {
            $location.path("/login");
            toast.pop({
                title: "Login",
                body: $translate("LOGIN_FAILED"),
                type: "error"
            });
        });
        $rootScope.$on(tt.authentication.logoutConfirmed, function () {
            $location.path("/login");
        });
    }]);