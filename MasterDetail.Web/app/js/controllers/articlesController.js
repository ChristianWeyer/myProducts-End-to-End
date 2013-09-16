define(["app"], function (app) {
    app.register.controller("ArticlesController",
        ["$scope", "$rootScope", "$location", "articlesApiService", "dataPushService", "alertService", "dialogService", "$translate", "personalizationService",
            function ($scope, $rootScope, $location, articlesApiService, dataPushService, alertService, dialogService, $translate, personalizationService) {
                $scope.capabilities = personalizationService.data.UiClaims.Capabilities;
                $scope.capabilities.has = function(key) {
                    return $scope.capabilities.indexOf(key) > -1;
                };

                $scope.articles = articlesApiService.getArticleList();

                dataPushService.on("articleChange", function () {
                    $scope.articles.read();
                });

                $scope.getArticleDetails = function (id) {
                    $location.path("/articledetails/" + id);
                };

                $scope.addArticle = function () {
                    $location.path("/articledetails/");
                };

                $scope.deleteArticle = function (id) {
                    articlesApiService.deleteArticle(id)
                        .success(function () {
                            $scope.articles.read();

                            alertService.pop({
                                title: $translate("POPUP_SUCCESS"),
                                body: $translate("POPUP_DELETED"),
                                type: "success"
                            });
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