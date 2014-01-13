(function () {

    /**
         * @param $scope
         * @param $routeParams
         * @param {$app.ArticlesApi} articlesApi
         * @param {$app.Toast} toast
         * @param $location
         * @param {$app.Dialog} dialog
         * @param $translate
         * @param {$app.Categories} categories
         */
    function Controller($scope, $routeParams, articlesApi, toast, $location, dialog, $translate, categories) {
        $scope.articledetails = {};
        $scope.articledetails.categories = categories.data;

        if ($routeParams.id !== "new") {
            ttTools.logger.info("Getting article details...");

            $scope.articledetails.editMode = true;

            articlesApi.getArticleDetails($routeParams.id)
                .success(function (data) {
                    $scope.articledetails.article = data;
                })
                .error(function (data) {
                    ttTools.logger.error("Server error", data);

                    dialog.showModalDialog({}, {
                        headerText: $translate("COMMON_ERROR"),
                        bodyText: $translate("DETAILS_ERROR"),
                        closeButtonText: $translate("COMMON_CLOSE"),
                        actionButtonText: $translate("COMMON_OK"),
                        detailsText: JSON.stringify(data)
                    });
                });
        }

        $scope.articledetails.save = function () {
            var file = $scope.articledetails.image ? $scope.articledetails.image.file : null;

            articlesApi.saveArticleWithImage($scope.articledetails.article, file)
                .success(function () {
                    toast.pop({
                        title: $translate("POPUP_SUCCESS"),
                        body: $translate("POPUP_SAVED"),
                        type: "success"
                    });
                    articlesApi.dataChanged();
                    $location.path('/articles');
                })
                .error(function (data, status, headers, config) {
                    $scope.modelState = data.ModelState;

                    if (status !== 422) {
                        ttTools.logger.error("Server error", data);

                        dialog.showModalDialog({}, {
                            headerText: $translate("COMMON_ERROR"),
                            bodyText: $translate("DETAILS_ERROR"),
                            closeButtonText: $translate("COMMON_CLOSE"),
                            actionButtonText: $translate("COMMON_OK"),
                            detailsText: JSON.stringify(data)
                        });
                    }
                });
        };
    };

    app.lazy.controller("ArticleDetailsController", ["$scope", "$routeParams", "articlesApi", "toast", "$location", "dialog", "$translate", "categories", Controller]);
})();
