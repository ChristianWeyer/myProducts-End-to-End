define(["app"], function (app) {
    app.factory("dataPushService", ["hubProxy", "$rootScope", function (hubProxy, $rootScope) {
        var hub = hubProxy(ttTools.baseUrl + "signalr", "clientNotificationHub");
        startHub();

        $rootScope.$on(tt.authentication.constants.loginConfirmed, function () {
            startHub();
        });
        $rootScope.$on(tt.authentication.constants.logoutConfirmed, function () {
            hub.stop();
        });

        hub.on("articleChange");
        
        function startHub() {
            if (ttTools.iOS()) {
                hub.start({ transport: "longPolling" });
            } else {
                hub.start();
            }
        }

        return hub;
    }]);
});