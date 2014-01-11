app.controller("StatusController", ["$scope", "networkStatus", "personalization", "tokenAuthentication",
    function ($scope, networkStatus, personalization, tokenAuthentication) {
        $scope.status = {};

        $scope.status.isOnline = networkStatus.isOnline();

        $scope.$on(tt.personalization.dataLoaded, function () {
            $scope.status.userName = personalization.data.UiClaims.UserName;
        });

        $scope.$on(tt.networkstatus.onlineChanged, function (evt, isOnline) {
            $scope.status.isOnline = isOnline;
        });

        $scope.status.logout = function () {
            tokenAuthentication.logout();
            $scope.status.userName = "";
        };
    }]);