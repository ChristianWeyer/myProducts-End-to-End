(function () {
    "use strict";

    /**
     * @param $localStorage
     * @constructor
     */
    function SettingsService ($localStorage) {
        var lsSettings = $localStorage.applicationSettings = $localStorage.applicationSettings || {};

        return lsSettings;
    };

    angular.module("myApp").factory("settingsService", ["$localStorage", SettingsService]);
})();
