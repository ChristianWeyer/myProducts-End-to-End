(function () {
    /**
     * @param $scope
     * @param $http
     */
    function Controller($scope, $http) {
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

    app.lazy.controller("GalleryController", ["$scope", "$http", Controller]);
})();
