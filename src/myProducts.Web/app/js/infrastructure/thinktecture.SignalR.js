var tt = window.tt || {}; tt.signalr = {};
tt.signalr = {
    subscribe: "tt:signalr:subscribe:"
};
    
angular.module("tt.SignalR", ["ng"]).
    factory("hubProxy", ["$rootScope", function ($rootScope) {
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
                        $rootScope.$broadcast(tt.signalr.subscribe + eventName, data);
                    });
                },
                off: function (eventName) {
                    proxy.off(eventName);
                },
                invoke: function () { // params methodName, argsForMethodName..., callback
                    var len = arguments.length;
                    var args = Array.prototype.slice.call(arguments); // convert to real array
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
