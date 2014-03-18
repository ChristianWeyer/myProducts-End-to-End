(function () {
    /**
     *
     */
    function Directive() {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                var obj = angular.element(document.querySelector(".bar-footer"));

                element.bind('focus', function () {
                    if (obj) {
                        obj.css("z-index", "-1");
                    }
                });

                element.bind('blur', function () {
                    if (obj) {
                        obj.css("z-index", "5");
                    }
                });
            }
        };
    };

    app.directive("fixViewport", Directive);
})();
