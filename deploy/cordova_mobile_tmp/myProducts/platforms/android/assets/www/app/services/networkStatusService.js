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

    app.service("networkStatusService", NetworkStatusService);
})();
