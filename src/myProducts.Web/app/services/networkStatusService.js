(function () {
    "use strict";

    /**
     * @constructor
     */
    function NetworkStatusService () {
        this.isOnline = function () {
            return navigator.onLine;
        };
    };

    angular.module("myApp").service("networkStatusService", NetworkStatusService);
})();
