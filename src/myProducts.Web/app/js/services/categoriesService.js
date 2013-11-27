app.factory("categories", ["$localStorage", function ($localStorage) {
    var categories = $localStorage.categories = $localStorage.categories || {};

    return categories;
}]);
