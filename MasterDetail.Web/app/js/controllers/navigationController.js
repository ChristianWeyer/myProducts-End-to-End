define(['app'], function (app) {
    app.controller('NavigationController', ['$http', '$translate', '$scope', '$rootScope', '$route', 'authenticationService',
        function ($http, $translate, $scope, $rootScope, $route, authenticationService) {

            $rootScope.$on(tt.authentication.constants.loggedIn, function () {
                $http({ method: "GET", url: ttTools.baseUrl + "api/modules" })
                .success(function (data) {
                    $scope.navigationItems = data;
                });
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