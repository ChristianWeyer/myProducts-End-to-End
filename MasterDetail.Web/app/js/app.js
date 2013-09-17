define(['services/routeResolver'], function () {
    var app = angular.module("myApp", ["ngRoute", "ngTouch", "ngAnimate", "$strap.directives", "ui.bootstrap", "kendo.directives", "ngSignalR", "tt.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices"]);

    app.config(["$routeProvider", "$locationProvider", "$translateProvider", "$httpProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider", "$provide",
        function ($routeProvider, $locationProvider, $translateProvider, $httpProvider, routeResolverProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {

            ttTools.initLogger(ttTools.baseUrl + "api/log");
            ttTools.logger.info("Configuring myApp...");

            app.register =
            {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service
            };

            routeResolverProvider.routeConfig.setBaseDirectories("app/views/", "app/js/controllers/");
            var route = routeResolverProvider.route;

            $routeProvider
                .when("/info", route.resolve("Info"))
                .when("/login", route.resolve("Login"))
                .otherwise({ redirectTo: "/" });

            $provide.factory('$routeProviderService', function () {
                return $routeProvider;
            });
            $provide.factory('routeResolverProviderService', function () {
                return routeResolverProvider;
            });

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

    app.run(["$http", "$templateCache", "$rootScope", "$location", "$translate", "alertService", "dialogService", "$route", "$routeProviderService", "routeResolverProviderService",
        function ($http, $templateCache, $rootScope, $location, $translate, alertService, dialogService, $route, $routeProviderService, routeResolverProviderService) {

            $rootScope.$on(tt.authentication.constants.loggedIn, function () {
                $http({ method: "GET", url: ttTools.baseUrl + "api/personalization" })
                .success(function (data) {
                    theSpinner.spin(document.getElementById("spinner"));
                    
                    tt.personalization.data = data;

                    var route = routeResolverProviderService.route;
                    var viewsDir = routeResolverProviderService.routeConfig.getViewsDirectory();

                    $http.get(viewsDir + "info.html", { cache: $templateCache });
                    
                    angular.forEach(data.Features, function (value, key) {
                        $routeProviderService.when(value.Url, route.resolve(value.Module));
                        $http.get(viewsDir + value.Module + ".html", { cache: $templateCache });
                    });
                    
                    $rootScope.$broadcast(tt.personalization.constants.dataLoaded);
                    
                    theSpinner.stop();
                });
            });

            // TODO: what about unloading!?
            
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

            var oldOpen = XMLHttpRequest.prototype.open;
            XMLHttpRequest.prototype.open = function (method, url, async, user, pass) {
                if (url.indexOf("/signalr") === -1) {
                    this.addEventListener("readystatechange", function () {
                        if (this.readyState === 1) {
                            theSpinner.spin(document.getElementById("spinner"));
                        }
                        ;
                        if (this.readyState === 4) {
                            theSpinner.stop();
                        }
                        ;
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
                    title: "Login",
                    body: $translate("LOGIN_FAILED"),
                    type: "error"
                });
            });
            $rootScope.$on(tt.authentication.constants.logoutConfirmed, function () {
                $location.path("/login");
            });
        }]);

    app.factory("loadingIndicatorInterceptor", function ($q) {
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

    return app;
});