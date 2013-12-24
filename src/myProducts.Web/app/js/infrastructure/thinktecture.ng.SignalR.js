
// version 0.1

angular.module("tt.SignalR", ["ng"]).value("subscribePrefix", "tt.signalr:subscribe");

angular.module("tt.SignalR").factory("hubProxy", ["$rootScope", "subscribePrefix", function ($rootScope, subscribePrefix) {
    function signalRHubProxyFactory(serverUrl, hubName) {
        var connection = $.hubConnection(serverUrl);
        connection.logging = true;
        var proxy = connection.createHubProxy(hubName);

        return {
            start: function (startOptions) {
                return connection.start(startOptions);
            },
            stop: function () {
                connection.stop();
                proxy = null;
            },
            on: function (eventName) {
                proxy.on(eventName, function (data) {
                    $rootScope.$apply(function () {
                        $rootScope.$broadcast(subscribePrefix + eventName, data);
                    });
                });
            },
            off: function (eventName) {
                proxy.off(eventName);
            },
            invoke: function () {
                var len = arguments.length;
                var args = Array.prototype.slice.call(arguments);
                var callback = undefined;

                if (len > 1) {
                    callback = args.pop();
                }

                proxy.invoke.apply(proxy, args)
                    .done(function (result) {
                        if (callback) {
                            $rootScope.$apply(function () {
                                callback(result);
                            });
                        }
                    });
            },
            connection: connection
        };
    };

    return signalRHubProxyFactory;
}]);
