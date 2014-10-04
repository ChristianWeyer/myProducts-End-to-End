(function () {
    "use strict";

    /**
     * @param $scope
     * @param $http
     * @constructor
     */
    function InfoController($scope, $http) {

    };

    appangular.module("myApp").controller("infoController", ["$scope", "$http", InfoController]);
})();
