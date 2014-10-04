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

    angular.module("myApp").filter("baseUrl", BaseUrlFilter);
})();
