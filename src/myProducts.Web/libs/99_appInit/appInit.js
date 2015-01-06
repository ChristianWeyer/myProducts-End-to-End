(function () {
    "use strict";

    window.app = window.app || { resolver: {} };

    if (ttMobile) {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "ngStorage", "chart.js", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    } else {
        app.module = angular.module("myApp", ["myApp.embeddedTemplates", "ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "mgcrea.ngStrap", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "chart.js", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    }

    app.module.config(function ($compileProvider, $translateProvider, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
        Offline.options = {
            checks: {
                xhr: {
                    url: ttTools.getBaseUrl()
                }
            },
            checkOnLoad: true,
            requests: false
        };

        $compileProvider.debugInfoEnabled(false);
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);

        FastClick.attach(document.body);

        cfpLoadingBarProvider.includeSpinner = false;

        tokenAuthenticationServiceProvider.setUrl(ttTools.baseUrl + "token");

        ttTools.initLogger(ttTools.baseUrl + "api/log");
        ttTools.logger.info("Configuring myApp...");

        $translateProvider.translations("de", tt.translations.de);
        $translateProvider.useStaticFilesLoader({
            prefix: "translations/locale-",
            suffix: ".json"
        });
        $translateProvider.preferredLanguage("de");
        $translateProvider.useLocalStorage();
    });
})();
