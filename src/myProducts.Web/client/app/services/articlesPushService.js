(function () {
    "use strict";

    /**
     * @param signalRHubProxy
     * @param $rootScope
     * @param {SettingsService} settingsService
     * @constructor
     */
    function ArticlesPushService(signalRHubProxy, $rootScope, $timeout, settingsService) {
        var hub = signalRHubProxy(ttTools.baseUrl, "clientNotificationHub");
        hub.on("articleChange");

        if (settingsService.enablePush) {
            ttTools.startHub(hub);
        }

        hub.connection.disconnected(function () {
            $timeout(function () {
                connection.start().done(function () {
                });
            }, 5000);
        });

        $rootScope.$on("oauth:authorized", function () {
            ttTools.startHub(hub);
        });
        $rootScope.$on("oauth:loggedOut", function () {
            ttTools.stopHub(hub);
        });

        $rootScope.$on("settingsService.enablePushChanged", function (evt, enable) {
            if (enable) {
                ttTools.startHub(hub);
            } else {
                ttTools.stopHub(hub);
            }
        });

        return hub;
    };

    app.module.factory("articlesPushService", ArticlesPushService);
})();
