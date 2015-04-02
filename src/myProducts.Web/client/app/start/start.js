(function () {
    "use strict";

    /**
     * @param $scope
     * @param {PersonalizationService} personalizationService
     * @constructor
     */
    function StartController($scope, AccessToken, $location, personalizationService) {
        $scope.start = {};
        $scope.start.classes = ["", "'bg-color-blue'", "'bg-color-blueDark'"];

        if (!AccessToken.get()) {
            $location.path("/login");
        }

        if (personalizationService.data) {
            $scope.start.navigationItems = getTileItems();
        }

        $scope.$on(tt.personalization.dataLoaded, function() {
            $scope.start.navigationItems = getTileItems();
        });

        function getTileItems() {
            return personalizationService.data.Features.filter(function(value, index) {
                return value.DisplayText;
            });
        }
    };

    app.module.controller("startController", StartController);
})();
