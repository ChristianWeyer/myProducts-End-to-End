    app.controller("SettingsController", ["$scope", "$rootScope", "settings", function ($scope, $rootScope, settings) {
        $scope.settings = $scope.settings || {};
        $scope.settings.enablePush = settings.enablePush;
        $scope.settings.enablePdfExport = settings.enablePdfExport;
        
        angular.forEach($scope.settings, function (value, key) {
            $scope.$watch("settings." + key, function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    settings[key] = newVal;
                    $rootScope.$broadcast("settings." + key + "Changed", newVal);
                }
            });
        });  
    }]);