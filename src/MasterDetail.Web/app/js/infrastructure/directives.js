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

    app.directive("fileInput", function ($parse) {
        return {
            restrict: "EA",
            template: "<input type='file' />",
            replace: true,
            link: function (scope, element, attrs) {
                $(element).filestyle({ input: false, buttonText: attrs.buttonText });

                // TODO: this is obviously not working... FIX IT
                attrs.$observe(attrs.buttonText, function (value) {
                    $(element).filestyle({'buttonText': value});
                });
                scope.$watch('buttonText', function (newValue) {
                    $(element).filestyle({'buttonText': newValue});
                });
                
                var modelGet = $parse(attrs.fileInput);
                var modelSet = modelGet.assign;
                var onChange = $parse(attrs.onChange);

                var updateModel = function () {
                    scope.$apply(function () {
                        modelSet(scope, element[0].files[0]);
                        onChange(scope);
                    });
                };

                element.bind("change", updateModel);
            }
        };
    });
});
