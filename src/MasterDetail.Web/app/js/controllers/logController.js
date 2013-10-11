define(["app"], function (app) {
    app.register.controller("LogController", ["$scope", "logPushService", function ($scope, logPushService) {
        $scope.logEntries = [];

        $scope.$on(tt.signalr.subscribe + "sendLogEvent", function (event, data) {
            $scope.$apply($scope.logEntries.push(data));
        });
    }]);
});