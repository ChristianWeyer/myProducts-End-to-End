(function () {
    "use strict";

    /**
     * @param $scope
     * @param $rootScope
     * @param {SettingsService} settingsService
     * @constructor
     */
    function SettingsController($scope, $rootScope, settingsService) {
        $scope.settings = {};
        $scope.settings.enablePush = settingsService.enablePush;
        $scope.settings.sendPosition = settingsService.sendPosition;

        angular.forEach($scope.settings, function (value, key) {
            $scope.$watch("settingsService." + key, function (newVal, oldVal) {
                if (newVal != oldVal) {
                    settingsService[key] = newVal;
                    $rootScope.$broadcast("settingsService." + key + "Changed", newVal);
                }
            });
        });
    };

    app.controller("settingsController", ["$scope", "$rootScope", "settingsService", SettingsController]);
})();
