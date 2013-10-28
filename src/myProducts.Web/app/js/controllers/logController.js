app.lazy.controller("LogController", ["$scope", "logPush", function ($scope, logPush) {
    $scope.logEntries = [];

    $scope.$on(tt.signalr.subscribe + "sendLogEvent", function (event, data) {
        $scope.$apply($scope.logEntries.push(data));
    });
}]);