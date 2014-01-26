(function () {
    /**
     * @param signalRHubProxy
     * @param $rootScope
     * @param {$app.Settings} settings
     */
    $app.ArticlesPush = function (signalRHubProxy, $rootScope, settings) {
        var hub = signalRHubProxy(ttTools.baseUrl, "clientNotificationHub");
        hub.on("articleChange");

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

    app.factory("articlesPush", ["signalRHubProxy", "$rootScope", "settings", $app.ArticlesPush]);
})();
