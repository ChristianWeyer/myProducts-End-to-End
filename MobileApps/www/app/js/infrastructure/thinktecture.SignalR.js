var tt = window.tt || {}; tt.signalr = {};
tt.signalr.constants = {
    subscribe: "tt:signalr:subscribe:"
};

angular.module("tt.SignalR", ["ng"]).
    factory("hubProxy", ["$rootScope", function ($rootScope) {
        function signalRHubProxyFactory(serverUrl, hubName) {
            var connection = $.hubConnection(serverUrl);
            connection.logging = true;
            var proxy;

            return {
                start: function (startOptions) {
                    proxy = connection.createHubProxy(hubName);

                    return connection.start(startOptions);
                },
                stop: function () {
                    connection.stop();
                    proxy = null;
                },
                on: function (eventName) {
                    proxy.on(eventName, function (data) {
                        $rootScope.$broadcast(tt.signalr.constants.subscribe + eventName, data);
                    });

                    //proxy.on(eventName, function () {
                    //    var eventNameArguments = arguments;
                    //    if (callback) {
                    //        $rootScope.$apply(function () {
                    //            callback.apply(callback, eventNameArguments);
                    //        });
                    //    }
                    //});
                },
                off: function (eventName, callback) {
                    proxy.off(eventName);

                    //proxy.off(eventName, function () {
                    //    var eventNameArguments = arguments;
                    //    if (callback) {
                    //        $rootScope.$apply(function () {
                    //            callback.apply(callback, eventNameArguments);
                    //        });
                    //    }
                    //});
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
