app.lazy.controller("LogController", ["$scope", "logPush", "subscribePrefix", function ($scope, logPush, subscribePrefix) {
    $scope.log = {};
    
    $scope.log.entries = [];

    $scope.$on(subscribePrefix + "sendLogEvent", function (event, data) {
        $scope.log.entries.push(data);
    });
}]);