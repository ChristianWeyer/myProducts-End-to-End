(function () {
    "use strict";

    /**
     * @param $scope
     * @param $http
     * @constructor
     */
    function GalleryController($scope, $http) {
        $scope.gallery = {};

        $scope.gallery.loadImages = function () {
            $http({
                method: "GET",
                url: ttTools.baseUrl + "api/images",
                cache: true
            }).then(function (response) {
                $scope.gallery.productImages = response.data;
            });
        };

        $scope.gallery.loadImages();
    };

    app.lazy.controller("galleryController", ["$scope", "$http", GalleryController]);
})();
