angular.module("tt.Authentication.Services", ["tt.Authentication.Internal"])
    .factory("authService", ["$rootScope", "$injector", "$q", "httpBuffer", function ($rootScope, $injector, $q, httpBuffer) {
        var store = new Lawnchair({ adapter: "dom", table: "authenticationToken" }, function () { });
        var key = "tt:authNToken";
        var $http;

        return {
            //authenticate: function(username, password) {
            //    var auth = "Basic " + Base64.encode(username + ":" + password);
                
            //    $http = $http || $injector.get("$http");
            //    $http.defaults.headers.common["Authorization"] = auth;
                
            //    $http.get(ttTools.baseUrl + "api/token").success(function (tokenData) {
            //        username = "";
            //        password = "";
            //        auth = "";

            //        this.setToken(tokenData);
            //        this.authenticationSuccess();
            //    });
            //},
            authenticationSuccess: function () {
                $rootScope.$broadcast("tt:authNConfirmed");
                httpBuffer.retry();
            },
            setToken: function (tokenData) {
                var expiration = new Date().getTime() + (tokenData.expires_in - 5) * 1000;
                var sessionTokenValue = "Session " + tokenData.access_token;

                $http = $http || $injector.get("$http");
                $http.defaults.headers.common["Authorization"] = sessionTokenValue;

                $(document).ajaxSend(function (event, xhr) {
                    xhr.setRequestHeader("Authorization", sessionTokenValue);
                });

                tokenData.expiration = expiration;
                store.save({ key: key, token: tokenData }, "");
            },
            getToken: function () {
                var deferred = $q.defer();

                store.get(key, function (token) {
                    deferred.resolve(token);
                });

                return deferred.promise;
            }
        };
    }]);

angular.module("tt.Authentication.Providers", ["tt.Authentication.Services", "tt.Authentication.Internal"])
    .config(["$httpProvider", function ($httpProvider) {
        var interceptor = ["$rootScope", "$q", "authService", "httpBuffer", function ($rootScope, $q, authService, httpBuffer) {
            $.ajaxPrefilter(function (options) {
                var thatError = options.error;
                var thatOptions = options;

                options.error = function (thisXhr, textStatus, errorThrown) {
                    if (typeof (thatError) === "function" && thisXhr.status != 401)
                        return thatError(thisXhr, textStatus, errorThrown);
                    else {
                        var deferred = $.Deferred();
                        $rootScope.$apply(checkForToken(thatOptions, deferred));
                    }
                };
            });

            function success(response) {
                return response;
            }

            function error(response) {
                if (response.status === 401) {
                    var deferred = $q.defer();
                    checkForToken(response, deferred);
                } else {
                    return $q.reject(response);
                }
            }

            function checkForToken(response, deferred) {
                authService.getToken().then(function (tokenData) {
                    httpBuffer.append(response, deferred);

                    if (!tokenData) {
                        $rootScope.$broadcast("tt:authNRequired");

                        return deferred.promise;
                    } else {
                        if (new Date().getTime() > tokenData.token.expiration) {
                            $rootScope.$broadcast("tt:authNRequired");

                            return deferred.promise;
                        } else {
                            authService.setToken(tokenData.token);
                            httpBuffer.retry();

                            return deferred.resolve(response);
                        }
                    }
                });
            }

            return function (promise) {
                return promise.then(success, error);
            };
        }];
        $httpProvider.responseInterceptors.push(interceptor);
    }]);

angular.module("tt.Authentication.Internal", [])
  .factory("httpBuffer", ["$injector", function ($injector) {
      var buffer = [];
      var $http;

      function retryHttpRequest(data, deferred) {
          function successCallback(response) {
              deferred.resolve(response);
          }

          function errorCallback(response) {
              deferred.reject(response);
          }

          if (data.config) {
              $http = $http || $injector.get("$http");
              $http(data.config).then(successCallback, errorCallback);
          } else {
              $.ajax(data).then(successCallback, errorCallback);
          }
      }

      return {
          append: function (data, deferred) {
              buffer.push({
                  data: data,
                  deferred: deferred
              });
          },

          retry: function () {
              for (var i = 0; i < buffer.length; ++i) {
                  retryHttpRequest(buffer[i].data, buffer[i].deferred);
              }
              buffer = [];
          }
      };
  }]);