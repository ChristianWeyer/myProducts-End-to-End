(function () {
    /**
     * @param $scope
     * @param tokenAuthentication
     * @param {$app.Toast} toast
     */
    function Controller($scope, tokenAuthentication, toast) {
        $scope.login = {};

        $scope.login.username = "";
        $scope.login.password = "";

        $scope.login.submit = function () {
            tokenAuthentication.login($scope.login.username, $scope.login.password)
                .error(function (data, status, headers, config) {
                    if (status !== 401) {
                        toast.pop({
                            title: "Login",
                            body: "Status: " + status,
                            type: "error"
                        });
                    };
                });
        };
    };

    app.controller("LoginController", ["$scope", "tokenAuthentication", "toast", Controller]);
})();