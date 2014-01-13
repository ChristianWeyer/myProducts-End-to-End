(function() {
    /**
     * @param $scope
     * @param $http
     */
    function Controller($scope, $http) {
        $scope.statistics = {};

        ttTools.logger.info("Freakin' cool stats!");

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/distribution"
        }).then(function(data) {
            $scope.statistics.pieSeries = data.data;
        });

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/sales"
        }).then(function(data) {
            $scope.statistics.columnSeries = data.data;
        });

        $scope.statistics.pieX = function() {
            return function(d) {
                return d.category;
            };
        };

        $scope.statistics.pieY = function() {
            return function(d) {
                return d.value;
            };
        };

        $scope.statistics.pieDescription = function() {
            return function(d) {
                return d.category;
            };
        };
    };

    app.lazy.controller("StatisticsController", ["$scope", "$http", Controller]);
})();
