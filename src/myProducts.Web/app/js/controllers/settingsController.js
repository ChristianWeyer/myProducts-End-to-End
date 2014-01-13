(function () {
    /**
     * @param $scope
     * @param $rootScope
     * @param {$app.Setings} settings
     */
    function Controller($scope, $rootScope, settings) {
        $scope.settings = {};
        $scope.settings.enablePush = settings.enablePush;
        $scope.settings.sendPosition = settings.sendPosition;

        angular.forEach($scope.settings, function (value, key) {
            $scope.$watch("settings." + key, function (newVal, oldVal) {
                if (newVal != oldVal) {
                    settings[key] = newVal;
                    $rootScope.$broadcast("settings." + key + "Changed", newVal);
                }
            });
        });
    };

    app.controller("SettingsController", ["$scope", "$rootScope", "settings", Controller]);
})();
