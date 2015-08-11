var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var MyProducts;
(function (MyProducts) {
    var InfoController = (function (_super) {
        __extends(InfoController, _super);
        function InfoController($scope) {
            _super.call(this, $scope);
            $scope.data = { message: "Hello out there." };
        }
        InfoController.prototype.clickMe = function () {
            alert(this.scope.data.message);
        };
        return InfoController;
    })(Thinktecture.Angular.Controller);
    MyProducts.InfoController = InfoController;
    app.module.controller("infoController", MyProducts.InfoController);
})(MyProducts || (MyProducts = {}));
//# sourceMappingURL=info.js.map