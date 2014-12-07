declare var app: any;

module MyProducts {
    export interface IInfoMessage {
        message: string;
    }

    export interface IInfoScope extends Thinktecture.Angular.IScope {
        data: IInfoMessage;
    }

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

    app.module.controller("infoController", ["$scope", "$http", MyProducts.InfoController]);
}
