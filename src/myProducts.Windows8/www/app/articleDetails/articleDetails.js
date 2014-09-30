(function () {
    "use strict";

    /**
         * @param $scope
         * @param $stateParams
         * @param {$app.ArticlesApi} articlesApi
         * @param {$app.Toast} toast
         * @param $location
         * @param {$app.Dialog} dialog
         * @param $translate
         * @param {$app.Categories} categories
         */
    function Controller($scope, $stateParams, articlesApi, toast, $location, dialog, $translate, categories) {
        $scope.articledetails = {};
        $scope.articledetails.categories = categories.data;

        var articleId = $stateParams.id;

        if (articleId !== "new") {
            ttTools.logger.info("Getting article details...");

            $scope.articledetails.editMode = true;

            articlesApi.getArticleDetails(articleId)
                .success(function (data) {
                    $scope.articledetails.article = data;
                })
                .error(function (data) {
                    ttTools.logger.error("Server error", data);

                    dialog.showModalDialog({}, {
                        headerText: $translate.instant("COMMON_ERROR"),
                        bodyText: $translate.instant("DETAILS_ERROR"),
                        closeButtonText: $translate.instant("COMMON_CLOSE"),
                        actionButtonText: $translate.instant("COMMON_OK"),
                        detailsText: JSON.stringify(data)
                    });
                });
        }

        $scope.articledetails.save = function () {
            var file = $scope.articledetails.image ? $scope.articledetails.image.file : null;

            articlesApi.saveArticleWithImage($scope.articledetails.article, file)
                .success(function () {
                    toast.pop({
                        title: $translate.instant("POPUP_SUCCESS"),
                        body: $translate.instant("POPUP_SAVED"),
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
                            headerText: $translate.instant("COMMON_ERROR"),
                            bodyText: $translate.instant("DETAILS_SAVE_ERROR"),
                            closeButtonText: $translate.instant("COMMON_CLOSE"),
                            actionButtonText: $translate.instant("COMMON_OK"),
                            detailsText: JSON.stringify(data)
                        });
                    }
                });
        };
    };

    app.lazy.controller("articleDetailsController", ["$scope", "$stateParams", "articlesApi", "toast", "$location", "dialog", "$translate", "categories", Controller]);
})();
