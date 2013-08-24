myApp.controller('NavigationController', ['$translate', '$scope', 'authenticationService', function ($translate, $scope, authenticationService) {
    $scope.changeLanguage = function (langKey) {
        $translate.uses(langKey);
    };

    $scope.logout = function() {
        authenticationService.logout();
    };
}]);