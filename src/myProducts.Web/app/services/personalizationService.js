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

    app.module.factory("personalizationService", PersonalizationService);
})();
