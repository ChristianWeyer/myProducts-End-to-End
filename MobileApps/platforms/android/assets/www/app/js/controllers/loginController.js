define(["app"], function (app) {
    app.controller("LoginController",
        ["$scope", "authenticationService", function($scope, authenticationService) {

            $scope.username = "";
            $scope.password = "";

            $scope.submit = function() {
                authenticationService.login(ttTools.baseUrl, $scope.username, $scope.password);
            };
        }]);
});