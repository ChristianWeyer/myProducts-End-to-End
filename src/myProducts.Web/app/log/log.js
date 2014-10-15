(function () {
    "use strict";

    /**
     * @param $scope
     * @param signalRSubscribe
     * @constructor
     */
    function LogController($scope, signalRSubscribe) {
        $scope.log = {};

        $scope.log.entries = [];

        $scope.$on(signalRSubscribe + "sendLogEvent", function (event, data) {
            $scope.log.entries.push(data);
        });
    };

    app.module.lazy.controller("logController", ["$scope", "signalRSubscribe", LogController]);
})();
