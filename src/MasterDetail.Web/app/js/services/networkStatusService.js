define(["app"], function (app) {
    app.factory("networkStatusService", function () {
        return {
            isOnline: function() {
                return navigator.onLine;
            }
        };
    });
});