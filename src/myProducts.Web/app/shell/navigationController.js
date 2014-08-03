(function () {
    "use strict";

    /**
     * @param $http
     * @param $scope
     * @param $translate
     * @param {$app.Personalization} personalization
     */
    function Controller($http, $scope, $translate, personalization, tokenAuthentication) {
        $scope.navigation = {};
        $scope.navigation.isCollapsed = true;

        $scope.navigation.currentLanguage = $translate.preferredLanguage() || $translate.proposedLanguage();

        $scope.navigation.toggleMenu = function () {
            $scope.sideMenuController.toggleLeft();
        };

        $scope.$on(tt.personalization.dataLoaded, function () {
            $scope.navigation.navigationItems = personalization.data.Features;
        });

        $scope.$on(tt.authentication.logoutConfirmed, function () {
            $scope.navigation.navigationItems = null;
        });

        $scope.navigation.changeLanguage = function (langKey) {
            $scope.navigation.currentLanguage = langKey;
            $translate.uses(langKey);
        };

        $scope.navigation.logout = function() {
            tokenAuthentication.logout();
        };
    };

    app.controller("navigationController", ["$http", "$scope", "$translate", "personalization", "tokenAuthentication", Controller]);
})();
