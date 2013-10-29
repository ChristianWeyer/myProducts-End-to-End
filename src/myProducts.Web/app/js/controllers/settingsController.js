    app.controller("SettingsController", ["$scope", "$rootScope", "settings", function ($scope, $rootScope, settings) {
        $scope.enablePush = settings.enablePush;
        $scope.enablePdfExport = settings.enablePdfExport;
        
        angular.forEach(settings, function (value, key) {
            $scope.$watch(key, function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    settings[key] = newVal;
                    $rootScope.$broadcast("settings." + key + "Changed", newVal);
                }
            });
        });  
    }]);