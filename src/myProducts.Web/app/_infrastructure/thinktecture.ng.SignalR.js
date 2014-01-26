//
// Thinktecture SignalR hubs clint module for AngularJS.
// Uses Microsoft's jQuery SignalR plugin. Dependency on SignalR version 2.0.2:
// https://github.com/SignalR/SignalR/releases/tag/2.0.2
// Version 0.2.0 - Jan 25, 2014.
//

angular.module("Thinktecture.SignalR", ["ng"]).value("signalRSubscribe", "tt.signalr:subscribe");

angular.module("Thinktecture.SignalR").factory("signalRHubProxy", ["$rootScope", "signalRSubscribe", function ($rootScope, subscribePrefix) {
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
