(function () {
    "use strict";

    window.app = window.app || { resolver: {} };

    if (ttMobile) {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "ngStorage", "chart.js", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    } else {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "mgcrea.ngStrap", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "chart.js", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    }

    app.module.config(["$urlRouterProvider", "$stateProvider", "$locationProvider", "$translateProvider", "$httpProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider", "$provide", "cfpLoadingBarProvider", "tokenAuthenticationServiceProvider",
        function ($urlRouterProvider, $stateProvider, $locationProvider, $translateProvider, $httpProvider, routeResolverProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
            FastClick.attach(document.body);

            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);

            cfpLoadingBarProvider.includeSpinner = false;

            tokenAuthenticationServiceProvider.setUrl(ttTools.baseUrl + "token");

            ttTools.initLogger(ttTools.baseUrl + "api/log");
            ttTools.logger.info("Configuring myApp...");

            app.module.lazy =
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
})();
