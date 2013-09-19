define(["app"], function (app) {
    app.register.controller("ArticleDetailsController",
        ["$scope", "$routeParams", "articlesApiService", "alertService", "$location", "dialogService", "$translate", function ($scope, $routeParams, articlesApiService, alertService, $location, dialogService, $translate) {

            if ($routeParams.id) {
                articlesApiService.getArticleDetails($routeParams.id)
                    .success(function (data) {
                        $scope.artikel = data;
                    })
                    .error(function (data, status, headers, config) {
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
                articlesApiService.saveArticle($scope.artikel)
                    .success(function () {
                        alertService.pop({
                            title: $translate("POPUP_SUCCESS"),
                            body: $translate("POPUP_SAVED"),
                            type: "success"
                        });
                        $location.path('/articles');
                    })
                    .error(function (data, status) {
                        if (status > 0) {
                            ttTools.logger.error("Server error", data);

                            dialogService.showModalDialog({}, {
                                headerText: $translate("COMMON_ERROR"),
                                bodyText: $translate("DETAILS_ERROR"),
                                closeButtonText: $translate("COMMON_CLOSE"),
                                actionButtonText: $translate("COMMON_OK"),
                                detailsText: JSON.stringify(data)
                            });
                        }
                    });
            };
        }]);
});