app.service("dialog", ["$modal", function ($modal) {
        var modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: "app/views/dialog.html"
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
        };
    }]);