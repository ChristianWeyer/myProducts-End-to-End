(function () {
    "use strict";

    /**
     * @param signalRHubProxy
     * @param $rootScope
     * @param {SettingsService} settingsService
     */
    function LogPushService (signalRHubProxy, $rootScope, settingsService) {
        var hub = signalRHubProxy(ttTools.baseUrl, "logHub");
        hub.on("sendLogEvent");

        if (settingsService.enablePush) {
            ttTools.startHub(hub);
        }

        $rootScope.$on(tt.authentication.loginConfirmed, function () {
            ttTools.startHub(hub);
        });
        $rootScope.$on(tt.authentication.logoutConfirmed, function () {
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

    app.module.factory("logPushService", LogPushService);
})();
