define(['app'], function (app) {
    app.directive('resetSourceWhen', function () {
        return function(scope, element, attrs) {
            var ds = element.inheritedData('$kendoDataSource');
            if (ds) {
                scope.$watch(attrs.resetSourceWhen, function(newVal, oldVal) {
                    if (newVal !== oldVal && newVal) {
                        ds.filter(null);
                    }
                });
            }
        };
    });
});