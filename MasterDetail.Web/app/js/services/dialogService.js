define(['app'], function (app) {
    app.service('dialogService', ['$dialog',
        function($dialog) {
            var dialogDefaults = {
                backdrop: true,
                keyboard: true,
                backdropClick: true,
                dialogFade: true,
                templateUrl: 'app/views/dialog.html'
            };

            var dialogOptions = {
                closeButtonText: 'Close',
                actionButtonText: 'OK',
                headerText: 'Proceed?',
                bodyText: 'Perform this action?',
                collapsed: true
            };

            this.showModalDialog = function(customDialogDefaults, customDialogOptions) {
                if (!customDialogDefaults) customDialogDefaults = {};
                customDialogDefaults.backdropClick = false;
                this.showDialog(customDialogDefaults, customDialogOptions);
            };

            this.showDialog = function(customDialogDefaults, customDialogOptions) {
                var tempDialogDefaults = {};
                var tempDialogOptions = {};

                angular.extend(tempDialogDefaults, dialogDefaults, customDialogDefaults);
                angular.extend(tempDialogOptions, dialogOptions, customDialogOptions);

                if (!tempDialogDefaults.controller) {
                    tempDialogDefaults.controller = function($scope, dialog) {
                        $scope.dialogOptions = tempDialogOptions;

                        $scope.dialogOptions.collapse = function() {
                            $scope.dialogOptions.collapsed = true;
                        };
                        $scope.dialogOptions.uncollapse = function() {
                            $scope.dialogOptions.collapsed = false;
                        };
                        $scope.dialogOptions.close = function(result) {
                            dialog.close(result);
                        };
                        $scope.dialogOptions.callback = function() {
                            dialog.close();

                            if (customDialogOptions && customDialogOptions.callback) {
                                customDialogOptions.callback();
                            }
                        };
                    };
                }

                var d = $dialog.dialog(tempDialogDefaults);
                d.open();
            };

            this.showMessage = function(title, message, buttons) {
                var defaultButtons = [{ result: 'ok', label: 'OK', cssClass: 'btn-primary' }];
                var msgBox = new $dialog.dialog({
                    dialogFade: true,
                    templateUrl: 'template/dialog/message.html',
                    controller: 'MessageBoxController',
                    resolve:
                    {
                        model: function() {
                            return {
                                title: title,
                                message: message,
                                buttons: buttons == null ? defaultButtons : buttons
                            };
                        }
                    }
                });
                return msgBox.open();
            };
        }]);
});