'use strict';

angular.module('ngSignalR', ['ng']).
    factory('hubProxy', ['$rootScope', function ($rootScope) {
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
                on: function (eventName, callback) {
                    proxy.on(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                off: function (eventName, callback) {
                    proxy.off(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                invoke: function (methodName, callback) {
                    proxy.invoke(methodName, arguments[1])
                        .done(function (result) {
                            $rootScope.$apply(function () {
                                if (callback && result) {
                                    callback(result);
                                }
                            });
                        });
                },
                connection: connection
            };
        };

        return signalRHubProxyFactory;
    }]);
