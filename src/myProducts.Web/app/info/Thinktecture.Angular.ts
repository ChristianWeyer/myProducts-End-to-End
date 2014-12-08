module Thinktecture.Angular {
    export interface IScope extends ng.IScope {
        events: Controller;
    }

    export class Controller {
        scope: IScope;

        constructor($scope: IScope) {
            this.scope = $scope;
            this.scope.events = this;
        }
    }
}
 