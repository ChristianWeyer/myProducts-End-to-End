(function () {
    "use strict";


    /**/
    var cacheStatusValues = [];
    cacheStatusValues[0] = 'uncached';
    cacheStatusValues[1] = 'idle';
    cacheStatusValues[2] = 'checking';
    cacheStatusValues[3] = 'downloading';
    cacheStatusValues[4] = 'updateready';
    cacheStatusValues[5] = 'obsolete';

    var cache = window.applicationCache;
    cache.addEventListener('cached', logEvent, false);
    cache.addEventListener('checking', logEvent, false);
    cache.addEventListener('downloading', logEvent, false);
    cache.addEventListener('error', logEvent, false);
    cache.addEventListener('noupdate', logEvent, false);
    cache.addEventListener('obsolete', logEvent, false);
    cache.addEventListener('progress', logEvent, false);
    cache.addEventListener('updateready', logEvent, false);

    function logEvent(e) {
        var online, status, type, message;
        online = (navigator.onLine) ? 'yes' : 'no';
        status = cacheStatusValues[cache.status];
        type = e.type;
        message = 'online: ' + online;
        message += ', event: ' + type;
        message += ', status: ' + status;
        if (type == 'error' && navigator.onLine) {
            message += ' (prolly a syntax error in manifest)';
        }
        console.log(message);
    }

    window.applicationCache.addEventListener(
        'updateready',
        function () {
            window.applicationCache.swapCache();
            console.log('swap cache has been called');
        },
        false
    );

    setInterval(function () { cache.update() }, 10000);

    window.app = window.app || { resolver: {} };

    if (ttMobile) {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "ngStorage", "chart.js", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    } else {
        app.module = angular.module("myApp", ["myApp.embeddedTemplates", "ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "mgcrea.ngStrap", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "chart.js", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    }

    app.module.config(function ($compileProvider, $translateProvider, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
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
