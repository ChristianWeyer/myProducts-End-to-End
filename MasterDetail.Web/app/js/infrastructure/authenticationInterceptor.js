angular.module("authenticationInterceptor", [])
.factory("authService", ["$rootScope", function ($rootScope) {
    return {
        authenticationSuccess: function () {
            $rootScope.$broadcast("tt:authNConfirmed");
        }
    };
}])
.config(["$httpProvider", function ($httpProvider) {
    var interceptor = ["$rootScope", "$q", function ($rootScope, $q) {
        function success(response) {
            return response;
        }

        function error(response) {
            if (response.status === 401) {
                var deferred = $q.defer();
                $rootScope.$broadcast("tt:authNRequired");
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