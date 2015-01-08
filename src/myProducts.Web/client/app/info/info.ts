declare var app: any;

module MyProducts {
    export class InfoController extends Thinktecture.Angular.Controller {
        scope: IInfoScope;

        constructor($scope: IInfoScope) {
            super($scope);
            $scope.data = { message: "Hello out there." };
        }

        clickMe() {
            alert(this.scope.data.message);
        }
    }

    app.module.controller("infoController", MyProducts.InfoController);
}
