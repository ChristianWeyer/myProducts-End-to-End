(function () {
    /**
     * @param $localStorage
     */
    $app.Settings = function ($localStorage) {
        var lsSettings = $localStorage.applicationSettings = $localStorage.applicationSettings || {};

        return lsSettings;
    };

    app.factory("settings", ["$localStorage", $app.Settings]);
})();
