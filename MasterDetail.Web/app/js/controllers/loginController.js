myApp.controller("LoginController",
    ["$scope", "$http", "$location", "authenticationService", "dialogService", function ($scope, $http, $location, authenticationService) {

        $scope.username = "";
        $scope.password = "";

        $scope.submit = function () {
            authenticationService.login($scope.username, $scope.password);
        };
    }]);
