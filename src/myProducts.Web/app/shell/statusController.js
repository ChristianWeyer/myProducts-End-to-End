(function() {
    /**
     * @param $scope
     * @param {$app.NetworkStatus} networkStatus
     * @param {$app.Personalization} personalization
     * @param tokenAuthentication
     */
    function Controller($scope, networkStatus) {
        $scope.status = {};
        $scope.status.isOnline = networkStatus.isOnline();

        $scope.$on(tt.networkstatus.onlineChanged, function (evt, isOnline) {
            $scope.status.isOnline = isOnline;
        });
    }

    app.controller("StatusController", ["$scope", "networkStatus", Controller]);
})();
