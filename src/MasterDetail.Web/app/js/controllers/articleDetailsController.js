define(["app"], function (app) {
    app.register.controller("ArticleDetailsController",
        ["$scope", "$routeParams", "articlesApiService", "alertService", "$location", "dialogService", "$translate",
            function ($scope, $routeParams, articlesApiService, alertService, $location, dialogService, $translate) {

                if ($routeParams.id) {
                    ttTools.logger.info("Getting article details...");

                    $scope.editMode = true;

                    articlesApiService.getArticleDetails($routeParams.id)
                        .success(function (data) {
                            $scope.artikel = data;
                        })
                        .error(function (data) {
                            ttTools.logger.error("Server error", data);

                            dialogService.showModalDialog({}, {
                                headerText: $translate("COMMON_ERROR"),
                                bodyText: $translate("DETAILS_ERROR"),
                                closeButtonText: $translate("COMMON_CLOSE"),
                                actionButtonText: $translate("COMMON_OK"),
                                detailsText: JSON.stringify(data)
                            });
                        });
                }

                $scope.save = function () {
                    articlesApiService.saveArticleWithImage($scope.artikel, $scope.image.file)
                        .success(function () {
                            alertService.pop({
                                title: $translate("POPUP_SUCCESS"),
                                body: $translate("POPUP_SAVED"),
                                type: "success"
                            });
                            $location.path('/articles');
                        })
                        .error(function (data) {
                            ttTools.logger.error("Server error", data);

                            dialogService.showModalDialog({}, {
                                headerText: $translate("COMMON_ERROR"),
                                bodyText: $translate("DETAILS_ERROR"),
                                closeButtonText: $translate("COMMON_CLOSE"),
                                actionButtonText: $translate("COMMON_OK"),
                                detailsText: JSON.stringify(data)
                            });
                        });
                };
            }]);
});