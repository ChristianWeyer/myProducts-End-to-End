(function () {
    'use strict';

    app.module.run(function ($localStorage, $state, $http, $templateCache, $rootScope, $location, $translate, toastService, dialogService, personalizationService, categoriesService, geoLocationTracker, articlesPushService, logPushService) {
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
            
            $rootScope.$on(tt.authentication.loggedIn, function () {
                $http({ method: "GET", url: ttTools.baseUrl + "api/personalization" })
                .success(function (data) {
                    if (!categoriesService.data) {
                        $http({ method: "GET", url: ttTools.baseUrl + "api/categories" })
                        .success(function (data) {
                            categoriesService.data = data;
                        });
                    }

                    personalizationService.data = data;
                    
                    $rootScope.$broadcast(tt.personalization.dataLoaded);
                    $location.path(currentPath);
                });
            });

            $rootScope.$on(tt.authentication.authenticationRequired, function () {
                $location.path("/login");
            });
            $rootScope.$on(tt.authentication.loginConfirmed, function () {
                $location.path("/");
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
        });
})();
