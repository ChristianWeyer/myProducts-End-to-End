define(["app"], function (app) {
    app.controller("NavigationController", ["$http", "$scope", "$translate", "authenticationService", "personalizationService",
        function ($http, $scope, $translate, authenticationService, personalizationService) {

            $scope.currentLanguage = $translate.preferredLanguage() || $translate.proposedLanguage();
            
            $scope.$on(tt.personalization.constants.dataLoaded, function () {
                $scope.navigationItems = personalizationService.data.Features;
            });

            $scope.$on(tt.authentication.constants.logoutConfirmed, function () {
                $scope.navigationItems = null;
            });

            $scope.changeLanguage = function (langKey) {
                $scope.currentLanguage = langKey;
                $translate.uses(langKey);
            };

            $scope.logout = function () {
                authenticationService.logout();
            };
        }]);
});