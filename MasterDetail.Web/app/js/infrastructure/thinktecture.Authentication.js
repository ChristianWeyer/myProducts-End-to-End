var tt = window.tt || {}; tt.authentication = {};
tt.authentication.constants = {
    authenticationRequired: "tt:authentication:authNRequired",
    loginConfirmed: "tt:authentication:loginConfirmed",
    loginFailed: "tt:authentication:loginFailed",
    loggedIn: "tt:authentication:loggedIn",
    logoutConfirmed: "tt:authentication:logoutConfirmed"
};

angular.module("tt.Authentication.Services", ["ng"])
    .factory("authenticationService", ["$rootScope", "$injector", "$q", function ($rootScope, $injector, $q) {
        var $http;
        var store = new Lawnchair({ adapter: "dom", table: "authenticationToken" }, function () { });
        var key = "tt:authentication:authNToken";
        var requestAttempts = 0;

        $rootScope.tt = $rootScope.tt || {}; $rootScope.tt.authentication = $rootScope.tt.authentication || {};
        $rootScope.tt.authentication.userLoggedIn = false;

        checkForValidToken($q.defer());

        function login(username, password) {
            var auth = "Basic " + tt.Base64.encode(username + ":" + password);

            $http = $http || $injector.get("$http");
            $http.defaults.headers.common["Authorization"] = auth;

            $http.get("/api/controller/token")
                .success(function (tokenData) {
                    username = "";
                    password = "";
                    auth = "";

                    setToken(tokenData);
                    authenticationSuccess();
                });
        }

        function logout() {
            $rootScope.tt.authentication.userLoggedIn = false;
            store.nuke();
            $rootScope.$broadcast(tt.authentication.constants.logoutConfirmed);
        }

        function authenticationSuccess() {
            requestAttempts = 0;
            $rootScope.tt.authentication.userLoggedIn = true;
            $rootScope.$broadcast(tt.authentication.constants.loginConfirmed);
            $rootScope.$broadcast(tt.authentication.constants.loggedIn);
        }

        function setToken(tokenData) {
            var expiration = new Date().getTime() + (tokenData.expires_in - 5) * 1000;
            var sessionTokenValue = "Session " + tokenData.access_token;

            $http = $http || $injector.get("$http");
            $http.defaults.headers.common["Authorization"] = sessionTokenValue;

            $(document).ajaxSend(function (event, xhr) {
                xhr.setRequestHeader("Authorization", sessionTokenValue);
            });

            tokenData.expiration = expiration;
            store.save({ key: key, token: tokenData }, "");
        }

        function getToken() {
            var deferred = $q.defer();

            store.get(key, function (token) {
                deferred.resolve(token);
            });

            return deferred.promise;
        }

        function checkForValidToken(deferred) {
            getToken().then(function (tokenData) {
                if (!tokenData) {
                    $rootScope.$broadcast(tt.authentication.constants.authenticationRequired);

                    return deferred.promise;
                } else {
                    if (new Date().getTime() > tokenData.token.expiration) {
                        $rootScope.$broadcast(tt.authentication.constants.authenticationRequired);

                        return deferred.promise;
                    } else {
                        setToken(tokenData.token);
                        $rootScope.tt.authentication.userLoggedIn = true;
                        $rootScope.$broadcast(tt.authentication.constants.loggedIn);

                        return deferred.promise;
                    }
                }
            });
        }

        return {
            login: login,
            logout: logout,
            authenticationSuccess: authenticationSuccess,
            checkForValidToken: checkForValidToken,
            setToken: setToken,
            getToken: getToken,
            requestAttempts: requestAttempts
        };
    }]);

angular.module("tt.Authentication.Providers", ["tt.Authentication.Services"])
    .config(["$httpProvider", function ($httpProvider) {
        var interceptor = ["$rootScope", "$q", "authenticationService", function ($rootScope, $q, authenticationService) {

            $.ajaxPrefilter(function (options) {
                var thatError = options.error;

                options.error = function (thisXhr, textStatus, errorThrown) {
                    if (thisXhr.status === 401) {
                        var deferred = $.Deferred();
                        $rootScope.tt.authentication.userLoggedIn = false;

                        if (authenticationService.requestAttempts > 0) {
                            $rootScope.$apply($rootScope.$broadcast(tt.authentication.constants.loginFailed));
                        } else {
                            authenticationService.requestAttempts++;
                            $rootScope.$apply(authenticationService.checkForValidToken(deferred));
                        }

                        return deferred.promise;
                    }
                    return thatError(thisXhr, textStatus, errorThrown);
                };
            });

            function success(response) {
                return response;
            }

            function error(response) {
                if (response.status === 401) {
                    var deferred = $q.defer();
                    $rootScope.tt.authentication.userLoggedIn = false;

                    if (authenticationService.requestAttempts > 0) {
                        $rootScope.$broadcast(tt.authentication.constants.loginFailed);

                        return $q.reject(response);
                    } else {
                        authenticationService.requestAttempts++;
                        authenticationService.checkForValidToken(deferred);
                    }

                    return deferred.promise;
                }

                return $q.reject(response);
            }

            return function (promise) {
                return promise.then(success, error);
            };
        }];

        $httpProvider.responseInterceptors.push(interceptor);
    }]);

tt.Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (n) { var f = "", e, t, i, s, h, o, r, u = 0; for (n = tt.Base64._utf8_encode(n) ; u < n.length;) e = n.charCodeAt(u++), t = n.charCodeAt(u++), i = n.charCodeAt(u++), s = e >> 2, h = (e & 3) << 4 | t >> 4, o = (t & 15) << 2 | i >> 6, r = i & 63, isNaN(t) ? o = r = 64 : isNaN(i) && (r = 64), f = f + this._keyStr.charAt(s) + this._keyStr.charAt(h) + this._keyStr.charAt(o) + this._keyStr.charAt(r); return f }, decode: function (n) { var t = "", e, o, s, h, u, r, f, i = 0; for (n = n.replace(/[^A-Za-z0-9\+\/\=]/g, "") ; i < n.length;) h = this._keyStr.indexOf(n.charAt(i++)), u = this._keyStr.indexOf(n.charAt(i++)), r = this._keyStr.indexOf(n.charAt(i++)), f = this._keyStr.indexOf(n.charAt(i++)), e = h << 2 | u >> 4, o = (u & 15) << 4 | r >> 2, s = (r & 3) << 6 | f, t = t + String.fromCharCode(e), r != 64 && (t = t + String.fromCharCode(o)), f != 64 && (t = t + String.fromCharCode(s)); return tt.Base64._utf8_decode(t) }, _utf8_encode: function (n) { var i, r, t; for (n = n.replace(/\r\n/g, "\n"), i = "", r = 0; r < n.length; r++) t = n.charCodeAt(r), t < 128 ? i += String.fromCharCode(t) : t > 127 && t < 2048 ? (i += String.fromCharCode(t >> 6 | 192), i += String.fromCharCode(t & 63 | 128)) : (i += String.fromCharCode(t >> 12 | 224), i += String.fromCharCode(t >> 6 & 63 | 128), i += String.fromCharCode(t & 63 | 128)); return i }, _utf8_decode: function (n) { for (var r = "", t = 0, i = c1 = c2 = 0; t < n.length;) i = n.charCodeAt(t), i < 128 ? (r += String.fromCharCode(i), t++) : i > 191 && i < 224 ? (c2 = n.charCodeAt(t + 1), r += String.fromCharCode((i & 31) << 6 | c2 & 63), t += 2) : (c2 = n.charCodeAt(t + 1), c3 = n.charCodeAt(t + 2), r += String.fromCharCode((i & 15) << 12 | (c2 & 63) << 6 | c3 & 63), t += 3); return r } };