app.register.controller("StatisticsController",
    ["$scope", function ($scope) {
        ttTools.logger.info("Freakin' cool stats!");

        // NOTE: Hard-coded sample values - go and fetch via Web API/localStorage
        $scope.pieSeries = [{
            type: "pie",
            startAngle: 150,
            data: [{
                category: "Asia",
                value: 33.8
            }, {
                category: "Europe",
                value: 36.1
            }, {
                category: "Latin America",
                value: 11.3
            }, {
                category: "Africa",
                value: 9.6
            }, {
                category: "Middle East",
                value: 5.2
            }, {
                category: "North America",
                value: 3.6
            }]
        }];

        $scope.columnSeries = [
        {
            name: "Total Sales",
            data: [56000, 63000, 74000]
        },
        {
            name: "Discounted Sales",
            data: [52000, 34000, 23000]
        }];
    }]);
