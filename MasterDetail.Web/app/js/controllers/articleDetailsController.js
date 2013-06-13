myApp.controller("ArticleDetailsController",
    ["$scope", "$routeParams", "articlesApiService", "alertService", "$location", function ($scope, $routeParams, articlesApiService, alertService, $location) {

    articlesApiService.getArticleDetails($routeParams.id)
        .success(function (data) {
            $scope.artikel = data;
        })
        .error(function (data, status) {
            if (status > 0) {
                console.log(status + " - " + data);
                alertService.pop({
                    title: "Error", body: data, type: "error"
                });
            }
        });

    $scope.save = function () {
        articlesApiService.saveArticle($scope.artikel)
            .success(function () {
                alertService.pop({
                    title: "Success", body: "Saved", type: "success"
                });
                $location.path('/');
            })
            .error(function (data, status) {
                if (status > 0) {
                    console.log(status + " - " + data);
                    alertService.pop({
                        title: "Error", body: data, type: "error"
                    });
                }
            });
    };
}]);
