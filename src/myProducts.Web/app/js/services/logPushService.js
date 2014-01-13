(function () {
    /**
     * @param hubProxy
     * @param $rootScope
     * @param {$app.Settings} settings
     */
    $app.LogPush = function (hubProxy, $rootScope, settings) {
        var hub = hubProxy(ttTools.baseUrl, "logHub");
        hub.on("sendLogEvent");

        if (settings.enablePush) {
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

    app.factory("logPush", ["hubProxy", "$rootScope", "settings", $app.LogPush]);
})();
