app.factory("GeoLocationTracker", function ($rootScope, $http, $timeout, phonegapReady) {
    var watchId;

    var location = {
        startSendPosition: phonegapReady(function (timeout, onSuccess, onError, options) {
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

                //promise = $timeout(poller, timeout);
            };

            poller();
        }),

        stopSendPosition: function () {
            //$timeout.cancel(promise);
            navigator.geolocation.clearWatch(watchId);
        }
    }

    return location;
});
