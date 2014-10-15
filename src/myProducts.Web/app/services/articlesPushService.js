(function () {
    "use strict";

    /**
     * @param signalRHubProxy
     * @param $rootScope
     * @param {SettingsService} settingsService
     * @constructor
     */
    function ArticlesPushService (signalRHubProxy, $rootScope, settingsService) {
        var hub = signalRHubProxy(ttTools.baseUrl, "clientNotificationHub");
        hub.on("articleChange");

        if (settingsService.enablePush) {
            ttTools.startHub(hub);
        }

        $rootScope.$on(tt.authentication.loginConfirmed, function () {
            ttTools.startHub(hub);
        });
        $rootScope.$on(tt.authentication.logoutConfirmed, function () {
            ttTools.stopHub(hub);
        });

        $rootScope.$on("settings.enablePushChanged", function (evt, enable) {
            if (enable) {
                ttTools.startHub(hub);
            } else {
                ttTools.stopHub(hub);
            }
        });

        return hub;
    };

    app.module.factory("articlesPushService", ["signalRHubProxy", "$rootScope", "settingsService", ArticlesPushService]);
})();
