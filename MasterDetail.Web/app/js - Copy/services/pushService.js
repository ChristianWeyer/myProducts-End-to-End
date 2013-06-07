myApp.factory("pushService", ["hubProxy", function (hubProxy) {
    var hub = hubProxy(ttTools.baseUrl + "signalr", "clientNotificationHub");
    hub.start();

    return hub;
}]);
