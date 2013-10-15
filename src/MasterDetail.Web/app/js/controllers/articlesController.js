define(["app"], function (app) {
    app.register.controller("ArticlesController",
        ["$scope", "$location", "articlesApi", "dataPush", "toast", "dialog", "$translate", "personalization",
            function ($scope, $location, articlesApi, dataPush, toast, dialog, $translate, personalization) {

                $scope.capabilities = personalization.data.UiClaims.Capabilities;
                $scope.capabilities.has = function (key) {
                    return $scope.capabilities.indexOf(key) > -1;
                };

                ttTools.logger.info("Loading articles...");
                $scope.articles = articlesApi.getArticleList();

                $scope.$on(tt.signalr.subscribe + "articleChange", function () {
                    $scope.articles.read();
                });

                $scope.getArticleDetails = function (id) {
                    $location.path("/articledetails/" + id);
                };

                $scope.addArticle = function () {
                    $location.path("/articledetails/new");
                };

                $scope.deleteArticle = function (id) {
                    articlesApi.deleteArticle(id)
                        .success(function () {
                            $scope.articles.read();

                            toast.pop({
                                title: $translate("POPUP_SUCCESS"),
                                body: $translate("POPUP_DELETED"),
                                type: "success"
                            });
                        })
                        .error(function (data, status) {
                            ttTools.logger.error("Server error", data);

                            dialog.showModalDialog({}, {
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