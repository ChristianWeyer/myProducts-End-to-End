define(["app"], function (app) {
    app.register.controller("GalleryController", ["$scope", "$http", function ($scope, $http) {
        
        $http({
            method: "GET",
            url: ttTools.baseUrl + "api/images"
        }).then(function (response) {
            $scope.productImages = response.data;
        });
    }]);
});