(function () {
    "use strict";

    /**
    * @param $http
    * @param $scope
    * @param $translate
    * @param {PersonalizationService} personalizationService
    * @param {TokenAuthenticationService} tokenAuthenticationService
    * @constructor
    */
    function NavigationController($http, $scope, $translate, personalizationService) {
        $scope.navigation = {};
        $scope.navigation.isCollapsed = true;

        $scope.navigation.currentLanguage = $translate.preferredLanguage() || $translate.proposedLanguage();

        $scope.navigation.toggleMenu = function () {
            $scope.sideMenuController.toggleLeft();
        };

        $scope.$on(tt.personalization.dataLoaded, function () {
            $scope.navigation.navigationItems = personalizationService.data.Features;
        });

        //$scope.$on(tt.authentication.logoutConfirmed, function () {
        //    $scope.navigation.navigationItems = null;
        //});

        $scope.navigation.changeLanguage = function (langKey) {
            $scope.navigation.currentLanguage = langKey;
            $translate.use(langKey);
        };

        //$scope.navigation.logout = function () {
        //    tokenAuthenticationService.logout();
        //};
    };

    function NavigationDirective() {
        return {
            restrict: "E",
            templateUrl: "navigation/navigation.html",
            controller: NavigationController
        };
    };

    app.module.controller("navigationController", NavigationController);
    app.module.directive("mypNavigation", NavigationDirective);
})();
