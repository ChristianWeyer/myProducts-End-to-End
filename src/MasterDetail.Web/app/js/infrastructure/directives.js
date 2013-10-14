define(["app"], function (app) {
    app.directive("resetSourceWhen", function () {
        return function (scope, element, attrs) {
            var ds = element.inheritedData("$kendoDataSource");

            if (ds) {
                scope.$watch(attrs.resetSourceWhen, function (newVal, oldVal) {
                    if (newVal !== oldVal && newVal) {
                        ds.filter(null);
                    }
                });
            }
        };
    });

    app.directive("chartAutoResize", function () {
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
});
