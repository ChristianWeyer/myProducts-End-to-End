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

    angular.module("myApp").directive("slidesRefresh", ["$ionicSlideBoxDelegate", Directive]);
})();
