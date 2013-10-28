app.lazy.controller("StatisticsController",
    ["$scope", "$http", "$timeout", function ($scope, $http, $timeout) {
        ttTools.logger.info("Freakin' cool stats!");

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/distribution"
        }).then(function (data) {
            $scope.pieSeries = data.data;
        });

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/sales"
        }).then(function (data) {
            $scope.columnSeries = data.data;
        });

        $scope.pieX = function () {
            return function (d) {
                return d.category;
            };
        };

        $scope.pieY = function () {
            return function (d) {
                return d.value;
            };
        };

        $scope.pieDescription = function () {
            return function (d) {
                return d.category;
            };
        };
    }]);
