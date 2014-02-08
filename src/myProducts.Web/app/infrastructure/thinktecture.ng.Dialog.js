// TODO: Provide set functions to allow setting the template paths

var tt = window.tt || {}; tt.dialog = {};
tt.dialog.module = angular.module("Thinktecture.Dialog", ["ng"]);

(function () {
    /**
     * @param $injector
     * @param $rootScope
     */
    var dialog = function ($injector, $rootScope) {
        var mobile = false;
        var $modal;

        try {
            $modal = $injector.get("$modal");
        } catch (e) {
            $modal = $injector.get("$ionicModal");
            mobile = true;
        }

        var modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: "app/infrastructure/dialog.html"
        };

        var modalOptions = {
            closeButtonText: "Close",
            actionButtonText: "OK",
            headerText: "Proceed?",
            bodyText: "Perform this action?",
            collapsed: true
        };

        this.showModalDialog = function (customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = "static";

            return this.showDialog(customModalDefaults, customModalOptions);
        };

        this.showDialog = function (customModalDefaults, customModalOptions) {
            var tempModalDefaults = {};
            var tempModalOptions = {};

            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);
            angular.extend(tempModalOptions, modalOptions, customModalOptions);

            if (!mobile) {
                if (!tempModalDefaults.controller) {
                    tempModalDefaults.controller = function ($scope, $modalInstance) {
                        $scope.dialogOptions = tempModalOptions;

                        $scope.dialogOptions.collapse = function () {
                            $scope.dialogOptions.collapsed = true;
                        };
                        $scope.dialogOptions.uncollapse = function () {
                            $scope.dialogOptions.collapsed = false;
                        };

                        $scope.dialogOptions.ok = function (result) {
                            $modalInstance.close(result);
                        };
                        $scope.dialogOptions.close = function () {
                            $modalInstance.dismiss("cancel");
                        };
                    };
                }

                return $modal.open(tempModalDefaults).result;
            } else {
                var mobileScope = $rootScope.$new(true);
                mobileScope.closeModal = function () {
                    mobileScope.modal.hide();
                };
                mobileScope.dialogOptions = tempModalOptions;

                $modal.fromTemplateUrl("mobile/infrastructure/dialog.html", function (modal) {
                    mobileScope.modal = modal;
                    mobileScope.modal.show();
                }, {
                    scope: mobileScope
                });
            }
        };
    };

    tt.dialog.module.service("dialog", ["$injector", "$rootScope", dialog]);
})();
