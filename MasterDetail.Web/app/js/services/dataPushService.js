myApp.factory("dataPushService", ["hubProxy", "$rootScope", function (hubProxy, $rootScope) {
    var hub = hubProxy(ttTools.baseUrl + "signalr", "clientNotificationHub");
    startHub();

    $rootScope.$on(tt.authentication.constants.loginConfirmed, function () {
        startHub();
    });
    $rootScope.$on(tt.authentication.constants.logoutConfirmed, function () {
        hub.stop();
    });
    
    function startHub() {
        hub.start({ transport: 'longPolling' });
    }
    
    return hub;
}]);
