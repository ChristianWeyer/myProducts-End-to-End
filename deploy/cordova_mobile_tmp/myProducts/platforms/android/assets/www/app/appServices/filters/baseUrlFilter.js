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

    app.filter("baseUrl", BaseUrlFilter);
})();
