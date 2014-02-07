//
// Thinktecture token-based authentication module for AngularJS.
// Implements OAuth2 resource owner password flow.
// Uses jQuery.
// Version 0.2.3 - Feb 7, 2014.
//

var tt = window.tt || {}; tt.authentication = {};
tt.authentication = {
    authenticationRequired: "tt:authentication:authNRequired",
    loginConfirmed: "tt:authentication:loginConfirmed",
    loginFailed: "tt:authentication:loginFailed",
    loggedIn: "tt:authentication:loggedIn",
    logoutConfirmed: "tt:authentication:logoutConfirmed"
};

tt.authentication.module = angular.module("Thinktecture.Authentication", ["ng"]);

tt.authentication.module.provider("tokenAuthentication", {
    storage: null,
    url: null,

    setStorage: function (s) {
        this.storage = s;
    },

    setUrl: function (u) {
        this.url = u;
    },

    $get: ["$rootScope", "$injector", "$q", function ($rootScope, $injector, $q) {
        var $http;
        var key = "tt:authentication:authNToken";
        var store;
        var that = this;

        if (this.storage === "private") {
            store = sessionStorage;
        } else {
            store = localStorage;
        }

        $rootScope.tt = $rootScope.tt || {}; $rootScope.tt.authentication = $rootScope.tt.authentication || {};
        $rootScope.tt.authentication.userLoggedIn = false;

        checkForValidToken();

        function login(username, password) {
            $http = $http || $injector.get("$http");
            var postData = $.param({ grant_type: "password", username: username, password: password });

            return $http({
                method: "POST",
                url: that.url,
                data: postData,
                headers: { "Content-Type": "application/x-www-form-urlencoded" }
            })
            .success(function (tokenData) {
                username = "";
                password = "";

                setToken(tokenData);
                authenticationSuccess();
            });
        }

        function logout() {
            store.removeItem(key);
            $rootScope.tt.authentication.userLoggedIn = false;
            $rootScope.$broadcast(tt.authentication.logoutConfirmed);
        }

        function authenticationSuccess() {
            $rootScope.tt.authentication.userLoggedIn = true;
            $rootScope.$broadcast(tt.authentication.loggedIn);
            $rootScope.$broadcast(tt.authentication.loginConfirmed);
        }

        function checkForValidToken() {
            getToken().then(function (tokenData) {
                $rootScope.tt.authentication.userLoggedIn = false;

                if (!tokenData) {
                    $rootScope.$broadcast(tt.authentication.authenticationRequired);

                    return false;
                } else {
                    if (new Date().getTime() > tokenData.expiration) {
                        $rootScope.$broadcast(tt.authentication.authenticationRequired);

                        return false;
                    } else {
                        setToken(tokenData);
                        $rootScope.tt.authentication.userLoggedIn = true;
                        $rootScope.$broadcast(tt.authentication.loggedIn);

                        return true;
                    }
                }
            });
        }

        function setToken(tokenData) {
            if (!tokenData.expiration) {
                var expiration = new Date().getTime() + (tokenData.expires_in - 500) * 1000;
                tokenData.expiration = expiration;
            }

            var sessionTokenValue = "Bearer " + tokenData.access_token;

            $http = $http || $injector.get("$http");
            $http.defaults.headers.common["Authorization"] = sessionTokenValue;

            store.setItem(key, JSON.stringify(tokenData));
        }

        function getToken() {
            var deferred = $q.defer();

            var token = JSON.parse(store.getItem(key));
            deferred.resolve(token);

            return deferred.promise;
        }

        return {
            login: login,
            logout: logout,
            checkForValidToken: checkForValidToken
        };
    }]
});

tt.authentication.module.factory("tokenAuthenticationHttpInterceptor", function ($q, $rootScope, tokenAuthentication) {
    function checkAuthenticationFailureStatus(deferred) {
        $rootScope.tt.authentication.userLoggedIn = false;

        if (tokenAuthentication.checkForValidToken()) {
            $rootScope.$broadcast(tt.authentication.authenticationRequired);
        } else {
            $rootScope.$broadcast(tt.authentication.loginFailed);
        }

        return deferred.promise;
    }

    return {
        "responseError": function (rejection) {
            if (rejection.status === 401) {
                checkAuthenticationFailureStatus($q.defer());
            }

            return $q.reject(rejection);
        }
    };
});

tt.authentication.module.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push("tokenAuthenticationHttpInterceptor");
}]);
