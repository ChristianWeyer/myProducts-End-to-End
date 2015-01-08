(function () {
    "use strict";

    function EastereggController() {

    }

    function MazeDirective() {
        return {
            restrict: "E",
            templateUrl: "easteregg/maze.html",
            link: function (scope, elem, attrs) {
                var contraption = new ThreeMaze($('.three').first());
            }
        };
    };

    app.module.controller("eastereggController", EastereggController);
    app.module.directive("eastereggMaze", MazeDirective);
})();
