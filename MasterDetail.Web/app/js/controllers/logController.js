define(['app'], function (app) {
    app.register.controller("LogController", ["$scope", "logPushService", function ($scope, logPushService) {
        $scope.logEntries = [];

        logPushService.on("sendLogEvent", function(logData) {
            $scope.logEntries.push(logData);
        });
    }]);
});