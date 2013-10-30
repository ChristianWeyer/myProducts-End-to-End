app.lazy.controller("LogController", ["$scope", "logPush", function ($scope, logPush) {
    $scope.log = {};
    
    $scope.log.entries = [];

    $scope.$on(tt.signalr.subscribe + "sendLogEvent", function (event, data) {
        $scope.$apply($scope.log.entries.push(data));
    });
}]);