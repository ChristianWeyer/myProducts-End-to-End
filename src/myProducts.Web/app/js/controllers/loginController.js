app.controller("LoginController",
    ["$scope", "tokenAuthentication", function ($scope, tokenAuthentication) {
        $scope.login = {};
        
        $scope.login.username = "";
        $scope.login.password = "";

        $scope.login.submit = function () {
            tokenAuthentication.login(ttTools.baseUrl, $scope.login.username, $scope.login.password);
        };
    }]);