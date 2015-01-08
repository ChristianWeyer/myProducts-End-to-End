(function () {
    "use strict";

    /**
     * @constructor
     */
    function BaseUrlFilter() {
        return function (input) {
            if (input) {
                return ttTools.baseUrl + input;
            }
        };
    };

    app.module.filter("baseUrl", BaseUrlFilter);
})();
