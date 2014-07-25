(function () {
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
        }).then(function (data) {
            $scope.statistics.pieSeries = data.data;
        });

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/sales"
        }).then(function (data) {
            $scope.statistics.columnSeries = data.data;
        });

        $scope.statistics.pieOptions = {
            chart: {
                type: 'pieChart',
                donut: true,
                donutRatio:0.3,
                height: 400,
                x: function (d) { return d.category; },
                y: function (d) { return d.value; },
                showLabels: true,
                labelType: 'percent',
                transitionDuration: 500,
                labelThreshold: 0.01,
                legend: {
                    margin: {
                        top: 5,
                        right: 35,
                        bottom: 5,
                        left: 0
                    }
                }
            }
        };

        $scope.statistics.columnOptions = {
            chart: {
                type: 'multiBarChart',
                height: 400,
                x: function (d) { return d.label; },
                y: function (d) { return d.value; },
                showControls: false,
                showValues: true,
                transitionDuration: 500
            }
        };
    };

    app.lazy.controller("StatisticsController", ["$scope", "$http", Controller]);
})();
