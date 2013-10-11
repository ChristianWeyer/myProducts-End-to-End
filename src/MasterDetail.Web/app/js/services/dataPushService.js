define(["app"], function (app) {
    app.factory("dataPushService", ["hubProxy", "$rootScope", "settingsService", function (hubProxy, $rootScope, settingsService) {
        var hub = hubProxy(ttTools.baseUrl + "signalr", "clientNotificationHub");
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

        $rootScope.$on(tt.settings.enablePushChanged, function (evt, enable) {
            if (enable) {
                ttTools.startHub(hub);
            } else {
                ttTools.stopHub(hub);
            }
        });

        return hub;
    }]);
});