app.lazy.controller("LogController", ["$scope", "subscribePrefix", function ($scope, subscribePrefix) {
    $scope.log = {};
    
    $scope.log.entries = [];

    $scope.$on(subscribePrefix + "sendLogEvent", function (event, data) {
        $scope.log.entries.push(data);
    });
}]);