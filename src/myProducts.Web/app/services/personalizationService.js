(function () {
    "use strict";

    /**
     * @param $localStorage
     * @constructor
     */
    function PersonalizationService ($localStorage) {
        var lsPersonalization = $localStorage.personalization = $localStorage.personalization || {};

        return lsPersonalization;
    };

    angular.module("myApp").factory("personalizationService", ["$localStorage", PersonalizationService]);
})();
