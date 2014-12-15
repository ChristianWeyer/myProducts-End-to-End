(function () {
    "use strict";

    /**
     * @param $scope
     * @param {NetworkStatusService} networkStatusService
     * @constructor
     */
    function StatusController($scope, networkStatusService) {
        $scope.status = {};
        $scope.status.isOnline = networkStatusService.isOnline();

        $scope.$on(tt.networkstatus.onlineChanged, function (evt, isOnline) {
            $scope.status.isOnline = isOnline;
        });
    }

    app.module.controller("statusController", StatusController);
})();
