define(["app"], function (app) {
    app.controller("LoginController",
        ["$scope", "tokenAuthentication", function ($scope, tokenAuthentication) {

            $scope.username = "";
            $scope.password = "";

            $scope.submit = function() {
                tokenAuthentication.login(ttTools.baseUrl, $scope.username, $scope.password);
            };
        }]);
});