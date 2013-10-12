define(["app"], function (app) {
    app.register.controller("StatisticsController",
        ["$scope", function ($scope) {
            ttTools.logger.info("Freakin' cool stats!");

            // NOTE: Hard-coded sample values - go and fetch via Web API/localStorage
            $scope.pieSeries = [{
                type: "pie",
                startAngle: 150,
                data: [{
                    category: "Asia",
                    value: 53.8,
                    color: "#9de219"
                }, {
                    category: "Europe",
                    value: 16.1,
                    color: "#90cc38"
                }, {
                    category: "Latin America",
                    value: 11.3,
                    color: "#068c35"
                }, {
                    category: "Africa",
                    value: 9.6,
                    color: "#006634"
                }, {
                    category: "Middle East",
                    value: 5.2,
                    color: "#004d38"
                }, {
                    category: "North America",
                    value: 3.6,
                    color: "#033939"
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
});
