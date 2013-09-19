define(["app"], function (app) {
    app.factory("logPushService", ["hubProxy", function (hubProxy) {
        var hub = hubProxy(ttTools.baseUrl + "signalr", "logHub");
        startHub();

        hub.on("sendLogEvent");
        
        function startHub() {
            hub.start({ transport: "longPolling" });
        }

        return hub;
    }]);
});