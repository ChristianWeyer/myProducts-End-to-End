(function () {
    'use strict';

    app.module.run(["$localStorage", "$stateProviderService", "$state", "$http", "$templateCache", "$rootScope", "$location", "$translate", "toastService", "dialogService", "routeResolver", "personalizationService", "categoriesService", "geoLocationTracker", "articlesPushService", "logPushService",
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

            $rootScope.$on(tt.authentication.authenticationRequired, function () {
                $location.path("/login");
            });
            $rootScope.$on(tt.authentication.loginConfirmed, function () {
                $location.path("/");

                //toast.pop({
                //    title: "Login",
                //    body: $translate.instant("LOGIN_SUCCESS"),
                //    type: "success"
                //});
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
})();
