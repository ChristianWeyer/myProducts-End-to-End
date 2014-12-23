(function () {
    "use strict";

    /**
     * @param $scope
     * @param {NetworkStatusService} networkStatusService
     * @constructor
     */
    function StatusBarController($scope, networkStatusService) {
        $scope.status = {};
        $scope.status.isOnline = networkStatusService.isOnline();

        $scope.$on(tt.networkstatus.onlineChanged, function (evt, isOnline) {
            $scope.status.isOnline = isOnline;
        });
    }

    function StatusBarDirective() {
        return {
            restrict: "E",
            templateUrl: "statusBar/statusBar.html",
            controller: StatusBarController
        };
    };

    app.module.controller("statusBarController", StatusBarController);
    app.module.directive("mypStatusbar", StatusBarDirective);
})();
