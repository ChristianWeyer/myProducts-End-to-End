app.lazy.controller("GalleryController", ["$scope", "$http", function ($scope, $http) {

    $scope.loadImages = function () {
        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/images",
            cache: true
        }).then(function (response) {
            $scope.productImages = response.data;
        });
    };

    $scope.loadImages();
}]);