app.register.controller("StatisticsController",
    ["$scope", "$http", "$timeout", function ($scope, $http, $timeout) {
        ttTools.logger.info("Freakin' cool stats!");

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/distribution"
        }).then(function (data) {
            $scope.pieSeries = [{
                type: "pie",
                startAngle: 150,
                data: data.data
            }];
        });

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/sales"
        }).then(function (data) {
            $scope.columnSeries = data.data;
        });
    }]);
