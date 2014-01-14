(function () {
    /**
     * @param $scope
     * @param $location
     * @param {$app.ArticlesApi} articlesApi
     * @param subscribePrefix
     * @param {$app.Toast} toast
     * @param {$app.Dialog} dialog
     * @param $translate
     * @param {$app.Personalization} personalization
     */
    function Controller($scope, $location, articlesApi, subscribePrefix, toast, dialog, $translate, personalization) {
        $scope.capabilities = personalization.data.UiClaims.Capabilities;
        $scope.capabilities.has = function (key) {
            return $scope.capabilities.indexOf(key) > -1;
        };

        $scope.articles = {};
        $scope.articles.pagingOptions = { pageSizes: [10], pageSize: 10, currentPage: 1 };

        $scope.articles.getFilteredData = function (searchText) {
            var search = searchText;

            return articlesApi.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, search, false)
                .then(function (data) {
                    $scope.articles.articlesData = data.Items;
                    $scope.articles.totalServerItems = data.Count;

                    return data.Items;
                }, function (data) {
                    dialog.showModalDialog({}, {
                        headerText: $translate("COMMON_ERROR"),
                        bodyText: $translate("DETAILS_ERROR"),
                        closeButtonText: $translate("COMMON_CLOSE"),
                        actionButtonText: $translate("COMMON_OK"),
                        detailsText: JSON.stringify(data)
                    });
                });
        };

        $scope.articles.getPagedData = function (force) {
            return articlesApi.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, "", force)
                .then(function (data) {
                    $scope.articles.articlesData = data.Items;
                    $scope.articles.totalServerItems = data.Count;
                    $scope.$broadcast("scroll.refreshComplete");
                }, function (data) {
                    dialog.showModalDialog({}, {
                        headerText: $translate("COMMON_ERROR"),
                        bodyText: $translate("DETAILS_ERROR"),
                        closeButtonText: $translate("COMMON_CLOSE"),
                        actionButtonText: $translate("COMMON_OK"),
                        detailsText: JSON.stringify(data)
                    });
                });
        };

        ttTools.logger.info("Loading articles...");

        $scope.articles.getPagedData(articlesApi.toBeForced);

        $scope.$watch("articles.pagingOptions", function (newVal, oldVal) {
            if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                if (newVal.currentPage < 1) {
                    $scope.articles.pagingOptions.currentPage = 1;
                } else {
                    $scope.articles.getPagedData();
                }
            }
        }, true);

        $scope.$on(subscribePrefix + "articleChange", function () {
            $scope.articles.getPagedData(true);
        });

        $scope.swipeLeft = function () {
            $scope.articles.pagingOptions.currentPage = $scope.articles.pagingOptions.currentPage + 1;
        };

        $scope.swipeRight = function () {
            $scope.articles.pagingOptions.currentPage = $scope.articles.pagingOptions.currentPage - 1;
        };

        $scope.articles.getArticleDetails = function (id) {
            $location.path("/articledetails/" + id);
        };

        $scope.articles.addArticle = function () {
            $location.path("/articledetails/new");
        };

        $scope.articles.deleteArticle = function (id) {
            articlesApi.deleteArticle(id)
                .success(function () {
                    $scope.articles.getPagedData(true);
                    $scope.articles.selectedFilteredArticle = null;

                    toast.pop({
                        title: $translate("POPUP_SUCCESS"),
                        body: $translate("POPUP_DELETED"),
                        type: "info"
                    });
                })
                .error(function (data, status, headers, config) {
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
    };

    app.lazy.controller("ArticlesController", ["$scope", "$location", "articlesApi", "subscribePrefix", "toast", "dialog", "$translate", "personalization", Controller]);
})();
