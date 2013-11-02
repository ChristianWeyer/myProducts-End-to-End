app.controller("LoginController",
    ["$scope", "tokenAuthentication", "toast", function ($scope, tokenAuthentication, toast) {
        $scope.login = {};
        
        $scope.login.username = "";
        $scope.login.password = "";

        $scope.login.submit = function () {
            tokenAuthentication.login(ttTools.baseUrl, $scope.login.username, $scope.login.password)
            .error(function (data, status, headers, config) {
                toast.pop({
                    title: "Login",
                    body: "Status: " + status,
                    type: "error"
                });
            });
        };
    }]);