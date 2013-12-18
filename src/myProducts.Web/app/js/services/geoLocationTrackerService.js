app.factory("GeoLocationTracker", function ($rootScope, $http, $timeout, phonegapReady) {
    var promise;

    var location = {
        startSendPosition: phonegapReady(function (timeout, onSuccess, onError, options) {
            var poller = function () {
                navigator.geolocation.getCurrentPosition(function () {
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
                        console.log("###GEOLOC: Error");

                        $rootScope.$apply(function () {
                            onError.apply(that, args);
                        });
                    }
                }, options);

                promise = $timeout(poller, timeout);
            };

            poller();
        }),

        stopSendPosition: function () {
            $timeout.cancel(promise);
        }
    }

    return location;
});
