(function () {
    "use strict";

    /**
     * @constructor
     */
    function NetworkStatusService($window, $rootScope) {
        $window.addEventListener("online", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, true));
        }, true);

        $window.addEventListener("offline", function () {
            $rootScope.$apply($rootScope.$broadcast(tt.networkstatus.onlineChanged, false));
        }, true);

        this.isOnline = function () {
            return navigator.onLine;
        };
    };

    app.module.service("networkStatusService", NetworkStatusService);
})();
