(function () {
    /**
     * @param $scope
     * @param subscribePrefix
     */
    function Controller($scope, subscribePrefix) {
        $scope.log = {};

        $scope.log.entries = [];

        $scope.$on(subscribePrefix + "sendLogEvent", function (event, data) {
            $scope.log.entries.push(data);
        });
    };

    app.lazy.controller("LogController", ["$scope", "subscribePrefix", Controller]);
})();
