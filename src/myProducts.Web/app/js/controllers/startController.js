app.controller("StartController", ["$scope", "personalization", function ($scope, personalization) {
    $scope.start = {};
    $scope.start.classes = ["", "'bg-color-blue'", "'bg-color-blueDark'"];

    if (personalization.data) {
        $scope.start.navigationItems = getTileItems();
    }
    
    $scope.$on(tt.personalization.dataLoaded, function () {
        $scope.start.navigationItems = getTileItems();
    });

    function getTileItems() {
        return personalization.data.Features.filter(function (value, index) {
            return value.DisplayText;
        });
    }
}]);