myApp.controller("LoginController",
    ["$scope", "$http", "$location", "authService", function ($scope, $http, $location, authService) {

        $scope.username = "";
        $scope.password = "";

        $scope.submit = function () {
            //authService.authenticate($scope.username, $scope.password);

            var auth = "Basic " + Base64.encode($scope.username + ":" + $scope.password);
            $http.defaults.headers.common["Authorization"] = auth;

            $http.get(ttTools.baseUrl + "api/token").success(function (tokenData) {
                $scope.username = "";
                $scope.password = "";
                auth = "";

                authService.setToken(tokenData);
                authService.authenticationSuccess();
            });
        };
    }]);
