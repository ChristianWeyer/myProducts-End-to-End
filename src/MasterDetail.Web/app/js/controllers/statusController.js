define(["app"], function (app) {
    app.controller("StatusController", ["$scope", "networkStatusService", "personalizationService", function ($scope, networkStatusService, personalizationService) {
        $scope.isOnline = networkStatusService.isOnline();
        
        $scope.$on(tt.personalization.constants.dataLoaded, function () {
            $scope.userName = personalizationService.data.UiClaims.UserName;
        });
        
        $scope.$on("tt:status:onlineChanged", function (evt, isOnline) {
            $scope.isOnline = isOnline;
        });
    }]);
});