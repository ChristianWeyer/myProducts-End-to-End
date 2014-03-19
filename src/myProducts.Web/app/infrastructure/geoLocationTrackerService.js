(function () {
    /**
     * @param $rootScope
     * @param $http
     * @param $timeout
     * @param phonegapReady
     */
    $app.GeoLocationTracker = function ($rootScope, $http, $timeout, phonegapReady) {
        var watchId;
        var enabled;
        var location = {};

        $rootScope.$on("settings.sendPositionChanged", function (evt, enable) {
            if (enable) {
                location.startSendPosition(10000, function (pos) { });
                enabled = true;
            } else {
                location.stopSendPosition();
                enabled = false;
            }
        });

        location.startSendPosition = phonegapReady(function (timeout, onSuccess, onError, options) {
            if (enabled) {
                var poller = function () {
                    var gpsOptions = {
                        enableHighAccuracy: true,
                        timeout: timeout,
                        maximumAge: 1000
                    };

                    // TODO: error handling (undefined)
                    watchId = navigator.geolocation.watchPosition(function () {
                        var that = this;
                        var args = arguments;

                        if (onSuccess) {
                            console.log("###GEOLOC: " + JSON.stringify(args));

                            $http.post(ttTools.baseUrl + "api/geolocation", {
                                data: args,
                                ignoreLoadingBar: true
                            });

                            $rootScope.$apply(function () {
                                onSuccess.apply(that, args);
                            });
                        }
                    }, function () {
                        var that = this;
                        var args = arguments;

                        if (onError) {
                            console.log("###GEOLOC: Error" + JSON.stringify(args));

                            $rootScope.$apply(function () {
                                onError.apply(that, args);
                            });
                        }
                    }, gpsOptions);
                };

                poller();
            }
        });

        location.stopSendPosition = function () {
            navigator.geolocation.clearWatch(watchId);
        };

        return location;
    };

    app.factory("geoLocationTracker", ["$rootScope", "$http", "$timeout", "phonegapReady", $app.GeoLocationTracker]);
})();
