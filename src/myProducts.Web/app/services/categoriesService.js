(function () {
    "use strict";

    /**
     * @param $localStorage
     * @constructor
     */
    function CatgoriesService ($localStorage) {
        var categories = $localStorage.categories = $localStorage.categories || {};

        return categories;
    };

    app.module.factory("categoriesService", ["$localStorage", CatgoriesService]);
})();
