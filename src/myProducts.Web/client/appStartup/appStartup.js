﻿(function () {
    'use strict';

    app.module.run(function (AccessToken, $localStorage, $state, $http, $templateCache, $rootScope, $location, $translate, toastService, dialogService, personalizationService, categoriesService, geoLocationTracker, articlesPushService, logPushService, networkStatusService) {
        geoLocationTracker.startSendPosition(10000, function (pos) { });

        //window.applicationCache.addEventListener("updateready", function (e) {
        //    if (window.applicationCache.status == window.applicationCache.UPDATEREADY) {
        //        if (confirm("A new version of this site is available. Load it?")) {
        //            window.location.reload();
        //        }
        //    } else {
        //    }
        //}, false);

        $http.defaults.headers.common["Accept-Language"] = $translate.proposedLanguage();
        $rootScope.$on("$translateChangeSuccess", function () {
            $http.defaults.headers.common["Accept-Language"] = $translate.proposedLanguage();
        });

        $rootScope.$on("oauth:authorized", function () {
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
            });
        });

        //if (!AccessToken.get()) {
        //    $location.path("/login");
        //}

        $rootScope.ttAppLoaded = true;
    });
})();
