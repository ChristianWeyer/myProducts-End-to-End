app.directive("ttChartAutoResize", function () {
    return function (scope, element, attrs) {
        $(window).resize(function () {
            var chartDiv = element;
            var chart = chartDiv.data("kendoChart");

            if (chart) {
                chart.options.transitions = false;
                chartDiv.css({ display: "none" });
                chartDiv.css({ width: chartDiv.parent().innerWidth(), display: "block" });
                chart.redraw();
            }
        });
    };
});