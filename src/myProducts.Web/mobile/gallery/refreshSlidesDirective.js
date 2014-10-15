(function () {
    "use strict";

    /**
     * @param $ionicSlideBoxDelegate
     */
    function Directive($ionicSlideBoxDelegate) {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                scope.$watch(attrs.slidesRefresh, function (newVal, oldVal) {
                    if (newVal != oldVal) {
                        $ionicSlideBoxDelegate.update();
                    }
                });
            }
        };
    };

    app.module.directive("slidesRefresh", ["$ionicSlideBoxDelegate", Directive]);
})();
