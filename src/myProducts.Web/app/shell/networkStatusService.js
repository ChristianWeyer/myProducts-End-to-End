(function () {
    "use strict";

    /**
     */
    $app.NetworkStatus = function () {
        this.isOnline = function () {
            return navigator.onLine;
        };
    };

    app.service("networkStatus", $app.NetworkStatus);
})();
