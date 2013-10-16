app.factory("settings", ["$localStorage", function ($localStorage) {
    var settings = $localStorage.applicationSettings = $localStorage.applicationSettings || {};

    return settings;
}]);