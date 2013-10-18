    app.controller("SettingsController", ["$scope", "$rootScope", "settings", function ($scope, $rootScope, settings) {
        $scope.enablePush = settings.enablePush;

        $scope.$watch("enablePush", function (newVal, oldVal) {
            if (newVal !== oldVal) {
                settings.enablePush = newVal;
                $rootScope.$broadcast(tt.settings.enablePushChanged, newVal);
            }
        });
    }]);