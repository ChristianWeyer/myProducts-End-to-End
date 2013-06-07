myApp.controller("LoginController",
    ["$scope", "$http", "$location", "authService", function ($scope, $http, $location, authService) {

    $scope.username = "";
    $scope.password = "";

    $scope.submit = function () {
        var auth = "Basic " + Base64.encode($scope.username + ":" + $scope.password);
        $http.defaults.headers.common["Authorization"] = auth;
        
        $http.get(ttTools.baseUrl + "api/token").success(function (data) {
            $scope.username = "";
            $scope.password = "";
            auth = "";
            
            var token = "Session " + data.access_token;
            
            $http.defaults.headers.common["Authorization"] = token;
            $(document).ajaxSend(function (event, xhr) {
                xhr.setRequestHeader("Authorization", token);
            });
            
            authService.authenticationSuccess();
        });
    };
}]);
