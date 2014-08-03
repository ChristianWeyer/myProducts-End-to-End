(function () {
    "use strict";

    /**
     * @param $scope
     * @param tokenAuthentication
     * @param {$app.Toast} toast
     */
    function Controller($scope, tokenAuthentication, dialog, $translate) {
        $scope.login = {};

        $scope.login.username = "";
        $scope.login.password = "";

        $scope.login.submit = function () {
            tokenAuthentication.login($scope.login.username, $scope.login.password)
                .error(function (data, status, headers, config) {
                    if (status === 400) {
                        dialog.showModalDialog({}, {
                            headerText: $translate("COMMON_ERROR"),
                            bodyText: $translate("LOGIN_FAILED"),
                            closeButtonText: $translate("COMMON_CLOSE"),
                            actionButtonText: $translate("COMMON_OK")
                        });
                    };
                });
        };
    };

    app.controller("loginController", ["$scope", "tokenAuthentication", "dialog", "$translate", Controller]);
})();
