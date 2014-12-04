var Thinktecture;
(function (Thinktecture) {
    var Angular;
    (function (Angular) {
        var Controller = (function () {
            function Controller($scope) {
                this.scope = $scope;
                this.scope.events = this;
            }
            return Controller;
        })();
        Angular.Controller = Controller;
    })(Angular = Thinktecture.Angular || (Thinktecture.Angular = {}));
})(Thinktecture || (Thinktecture = {}));
//# sourceMappingURL=Thinktecture.Angular.js.map