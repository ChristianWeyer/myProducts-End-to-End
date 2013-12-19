app.factory("GeoLocationTracker", function ($rootScope, $http, $timeout, phonegapReady) {
    var watchId;
    var enabled;

    $rootScope.$on("settings.sendPosition", function (evt, enable) {
        if (enable) {
            location.startSendPosition(10000, function (pos) { });
            enabled = true;
        } else {
            location.stopSendPosition();
            enabled = false;
        }
    });

    var location = {
        startSendPosition: phonegapReady(function (timeout, onSuccess, onError, options) {
            if (enabled) {
                var poller = function () {
                    var gpsOptions = {
                        enableHighAccuracy: true,
                        timeout: timeout,
                        maximumAge: 1000
                    };

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
        }),

        stopSendPosition: function () {
            navigator.geolocation.clearWatch(watchId);
        }
    }

    return location;
});
