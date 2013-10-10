define(["app"], function (app) {
    app.factory("logPushService", ["hubProxy", function (hubProxy) {
        var hub = hubProxy(ttTools.baseUrl + "signalr", "logHub");
        startHub();

        hub.on("sendLogEvent");
        
        function startHub() {
            //if (ttTools.iOS()) {
            //    hub.start({ transport: "longPolling" });
            //} else {
                hub.start();
            //}
        }

        return hub;
    }]);
});