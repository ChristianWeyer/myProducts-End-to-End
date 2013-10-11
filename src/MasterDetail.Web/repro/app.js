var app = angular.module("app", ["angular-carousel"]);

app.controller("GalleryController", ["$scope", "$http", function ($scope, $http) {

    $scope.loadImages = function () {
        $http({
            method: "GET",
            url: "../api/images",
            cache: true
        }).then(function (response) {
            $scope.productImages = response.data;
        });
    };

    alert("Go ahead and fire up up debugging tools... ;)");
    $scope.loadImages();
}]);