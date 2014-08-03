(function () {
    "use strict";

    /**
     * @param $localStorage
     */
    $app.Catgories = function ($localStorage) {
        var categories = $localStorage.categories = $localStorage.categories || {};

        return categories;
    };

    app.factory("categories", ["$localStorage", $app.Catgories]);
})();
