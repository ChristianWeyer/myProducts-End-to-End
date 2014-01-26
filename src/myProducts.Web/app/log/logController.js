(function () {
    /**
     * @param $scope
     * @param signalRSubscribe
     */
    function Controller($scope, signalRSubscribe) {
        $scope.log = {};

        $scope.log.entries = [];

        $scope.$on(signalRSubscribe + "sendLogEvent", function (event, data) {
            $scope.log.entries.push(data);
        });
    };

    app.lazy.controller("LogController", ["$scope", "signalRSubscribe", Controller]);
})();
