define(['app'], function (app) {
    app.register.controller("ArticleDetailsController",
        ["$scope", "$routeParams", "articlesApiService", "alertService", "$location", "dialogService", "$translate", function ($scope, $routeParams, articlesApiService, alertService, $location, dialogService, $translate) {

            articlesApiService.getArticleDetails($routeParams.id)
                .success(function (data) {
                    $scope.artikel = data;
                })
                .error(function (data, status, headers, config) {
                    ttTools.logger.error("Server error", data);

                    dialogService.showModalDialog({}, {
                        headerText: $translate("COMMON_ERROR"),
                        bodyText: $translate("DETAILS_ERROR"),
                        detailsText: JSON.stringify(data)
                    });
                });

            $scope.save = function () {
                articlesApiService.saveArticle($scope.artikel)
                    .success(function () {
                        alertService.pop({
                            title: "Success",
                            body: "Saved",
                            type: "success"
                        });
                        $location.path('/articles');
                    })
                    .error(function (data, status) {
                        if (status > 0) {
                            console.log(status + " - " + data);

                            alertService.pop({
                                title: "Error",
                                body: data,
                                type: "error"
                            });
                        }
                    });
            };
        }]);
});