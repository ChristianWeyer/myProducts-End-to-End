myApp.controller("ArticleDetailsController",
    ["$scope", "$routeParams", "articlesApiService", "alertService", "$location", "dialogService", function ($scope, $routeParams, articlesApiService, alertService, $location, dialogService) {

        articlesApiService.getArticleDetails($routeParams.id)
            .success(function (data) {
                $scope.artikel = data;
            })
            //.error(function (data, status) {
            //    if (status > 0) {
            //        console.log(status + " - " + data);
            //        alertService.pop({
            //            title: "Error", body: data, type: "error"
            //        });
            //    }
            //});
            .error(function (data, status, headers, config) {
                dialogService.showModalDialog({}, {
                    headerText: 'Error',
                    bodyText: 'Article details could not be loaded (see details).',
                    detailsText: JSON.stringify(data)
                });
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
