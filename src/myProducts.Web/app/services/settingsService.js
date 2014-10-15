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

    app.module.factory("settingsService", ["$localStorage", SettingsService]);
})();
