app.controller("StartController", ["$scope", "personalization", function ($scope, personalization) {
    $scope.start = {};
    $scope.start.navigationItems = personalization.data.Features.filter(function (value, index) {
        return value.DisplayText;
    });
    $scope.start.classes = ["", "'bg-color-blue'", "'bg-color-blueDark'"];
}]);