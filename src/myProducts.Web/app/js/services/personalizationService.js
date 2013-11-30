app.factory("personalization", ["$localStorage", function ($localStorage) {
    var personalization = $localStorage.personalization = $localStorage.personalization || {};

    return personalization;
}]);
