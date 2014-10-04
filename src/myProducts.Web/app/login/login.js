(function () {
    "use strict";

    /**
     * @param $scope
     * @param {TokenAuthenticationService} tokenAuthenticationService
     * @param {DialogService} dialogService
     * @constructor
     */
    function LoginController($scope, tokenAuthenticationService, dialogService, $translate) {
        $scope.login = {};

        $scope.login.username = "";
        $scope.login.password = "";

        $scope.login.submit = function () {
            tokenAuthenticationService.login($scope.login.username, $scope.login.password)
                .error(function (data, status, headers, config) {
                    if (status === 400) {
                        dialogService.showModalDialog({}, {
                            headerText: $translate.instant("COMMON_ERROR"),
                            bodyText: $translate.instant("LOGIN_FAILED"),
                            closeButtonText: $translate.instant("COMMON_CLOSE"),
                            actionButtonText: $translate.instant("COMMON_OK")
                        });
                    };
                });
        };
    };

    angular.module("myApp").controller("loginController", ["$scope", "tokenAuthenticationService", "dialogService", "$translate", LoginController]);
})();
