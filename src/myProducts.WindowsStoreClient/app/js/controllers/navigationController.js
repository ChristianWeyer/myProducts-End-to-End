(function () {
    /**
     * @param $http
     * @param $scope
     * @param $translate
     * @param {$app.Personalization} personalization
     */
    function Controller($http, $scope, $translate, personalization) {
        $scope.navigation = {};
        $scope.navigation.isCollapsed = true;

        $scope.navigation.currentLanguage = $translate.preferredLanguage() || $translate.proposedLanguage();

        $scope.navigation.openLeft = function () {
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
    };

    app.controller("NavigationController", ["$http", "$scope", "$translate", "personalization", Controller]);
})();
