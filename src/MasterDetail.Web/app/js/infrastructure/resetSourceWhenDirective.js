app.directive("ttResetSourceWhen", function () {
    return function (scope, element, attrs) {
        var ds = element.inheritedData("$kendoDataSource");

        if (ds) {
            scope.$watch(attrs.ttResetSourceWhen, function (newVal, oldVal) {
                if (newVal !== oldVal && newVal) {
                    ds.filter(null);
                }
            });
        }
    };
});