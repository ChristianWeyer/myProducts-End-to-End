define(["app"], function (app) {
    app.factory("settingsService", ["$localStorage", function ($localStorage) {
        var settings = $localStorage.applicationSettings = $localStorage.applicationSettings || {};

        return settings;
    }]);
});