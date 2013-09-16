define(["app"], function (app) {
    app.controller("NavigationController", ["$http", "$scope", "$rootScope", "$translate", "authenticationService", "personalizationService",
        function ($http, $scope, $rootScope, $translate, authenticationService, personalizationService) {
            
            $rootScope.$on(tt.personalization.constants.dataLoaded, function () {
                $scope.userName = personalizationService.data.UiClaims.UserName;
                $scope.navigationItems = personalizationService.data.Features;
            });

            $rootScope.$on(tt.authentication.constants.logoutConfirmed, function () {
                $scope.navigationItems = null;
            });

            $scope.changeLanguage = function (langKey) {
                $translate.uses(langKey);
            };

            $scope.logout = function () {
                authenticationService.logout();
            };
        }]);
});