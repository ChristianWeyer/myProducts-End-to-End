myApp.controller("LoginController",
    ["$scope", "$http", "$location", "authService", function ($scope, $http, $location, authService) {

        $scope.username = "";
        $scope.password = "";

        $scope.submit = function () {
            authService.authenticate($scope.username, $scope.password);
        };
    }]);
