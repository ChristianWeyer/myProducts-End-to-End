(function () {
    "use strict";

    /**
         * @param $scope
         * @param $stateParams
         * @param {ArticlesService} articlesService
         * @param {Toast} toastService
         * @param $location
         * @param {DialogService} dialogService
         * @param $translate
         * @param {CategoriesService} categoriesService
         * @constructor
         */
    function ArticleDetailsController($scope, $stateParams, articlesService, toastService, $location, dialogService, $translate, categoriesService) {
        $scope.articledetails = {};
        $scope.articledetails.categories = categoriesService.data;

        var articleId = $stateParams.id;

        if (articleId !== "new") {
            ttTools.logger.info("Getting article details...");

            $scope.articledetails.editMode = true;

            articlesService.getArticleDetails(articleId)
                .success(function (data) {
                    $scope.articledetails.article = data;
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

        $scope.articledetails.save = function () {
            var file = $scope.articledetails.image ? $scope.articledetails.image.file : null;

            articlesService.saveArticleWithImage($scope.articledetails.article, file)
                .success(function () {
                    toastService.pop({
                        title: $translate("POPUP_SUCCESS"),
                        body: $translate("POPUP_SAVED"),
                        type: "success"
                    });

                    articlesService.dataChanged();
                    $location.path('/articles');
                })
                .error(function (data, status, headers, config) {
                    $scope.modelState = data.ModelState;

                    if (status !== 422) {
                        ttTools.logger.error("Server error", data);

                        dialogService.showModalDialog({}, {
                            headerText: $translate("COMMON_ERROR"),
                            bodyText: $translate("DETAILS_SAVE_ERROR"),
                            closeButtonText: $translate("COMMON_CLOSE"),
                            actionButtonText: $translate("COMMON_OK"),
                            detailsText: JSON.stringify(data)
                        });
                    }
                });
        };
    };

    app.lazy.controller("articleDetailsController", ["$scope", "$stateParams", "articlesService", "toastService", "$location", "dialogService", "$translate", "categoriesService", ArticleDetailsController]);
})();
