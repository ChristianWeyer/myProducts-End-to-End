define(["app"], function (app) {
    app.controller("LoginController",
        ["$scope", "$http", "$location", "authenticationService", "dialogService", function($scope, $http, $location, authenticationService) {

            $scope.username = "";
            $scope.password = "";

            $scope.submit = function() {
                authenticationService.login(ttTools.baseUrl, $scope.username, $scope.password);
            };
        }]);
});