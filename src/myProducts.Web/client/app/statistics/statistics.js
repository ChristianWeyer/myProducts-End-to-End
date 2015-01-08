(function () {
    "use strict";

    /**
     * @param $scope
     * @param $http
     * @constructor
     */
    function StatisticsController($scope, $http) {
        $scope.statistics = {};

        ttTools.logger.info("Freakin' cool stats!");

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/distribution"
        }).then(function (response) {
            $scope.statistics.pieData = response.data.Data;
            $scope.statistics.pieLabels = response.data.Labels;
        });

        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/statistics/sales"
        }).then(function (response) {
                $scope.statistics.columnData = response.data.Data;
                $scope.statistics.columnSeries = response.data.Series;
                $scope.statistics.columnLabels = response.data.Labels;
        });
    };

    app.module.controller("statisticsController", StatisticsController);
})();
