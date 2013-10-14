define(["app"], function (app) {
    app.controller("StatusController", ["$scope", "networkStatus", "personalization", "tokenAuthentication",
        function ($scope, networkStatus, personalization, tokenAuthentication) {
            $scope.isOnline = networkStatus.isOnline();

            $scope.$on(tt.personalization.dataLoaded, function () {
                $scope.userName = personalization.data.UiClaims.UserName;
            });

            $scope.$on(tt.networkstatus.onlineChanged, function (evt, isOnline) {
                $scope.isOnline = isOnline;
            });

            $scope.logout = function () {
                tokenAuthentication.logout();
            };
        }]);
});