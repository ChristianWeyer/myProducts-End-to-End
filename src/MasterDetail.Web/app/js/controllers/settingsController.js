define(["app"], function (app) {
    app.controller("SettingsController", ["$scope", "$rootScope", "settingsService", function ($scope, $rootScope, settingsService) {
        $scope.enablePush = settingsService.enablePush;

        $scope.$watch("enablePush", function (newVal, oldVal) {
            if (newVal !== oldVal) {
                settingsService.enablePush = newVal;
                $rootScope.$broadcast(tt.settings.enablePushChanged, newVal);
            }
        });
    }]);
});