define(['app'], function (app) {
    app.factory("logPushService", ["hubProxy", function (hubProxy) {
        var hub = hubProxy(ttTools.baseUrl + "signalr", "logHub");
        startHub();

        function startHub() {
            hub.start({ transport: 'longPolling' });
        }

        return hub;
    }]);
});