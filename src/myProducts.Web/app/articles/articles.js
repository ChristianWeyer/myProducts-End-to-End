(function () {
    "use strict";

    /**
     * @param $scope
     * @param $location
     * @param {ArticlesService} articlesService
     * @param signalRSubscribe
     * @param {ToastService} toastService
     * @param {DialogService} dialogService
     * @param $translate
     * @param {PersonalizationService} personalizationService
     */
    function ArticlesController($scope, $location, articlesService, signalRSubscribe, toastService, dialogService, $translate, personalizationService) {
        $scope.capabilities = personalizationService.data.UiClaims.Capabilities;
        $scope.capabilities.has = function (key) {
            return $scope.capabilities.indexOf(key) > -1;
        };

        $scope.articles = {};
        $scope.articles.pagingOptions = { pageSizes: [10], pageSize: 10, currentPage: 1, moreCurrentPage: 1 };
        $scope.articles.articlesData = [];

        $scope.articles.getFilteredData = function (searchText) {
            var search = searchText;

            return articlesService.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, search, false)
                .then(function (data) {
                    $scope.articles.articlesData = data.Items;
                    $scope.articles.totalServerItems = data.Count;

                    return data.Items;
                }, function (data) {
                    showError(data);
                });
        };

        $scope.articles.getMoreData = function () {
            articlesService.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.moreCurrentPage += 1, "", true)
                .then(function (data) {
                    $scope.articles.articlesData.push.apply($scope.articles.articlesData, data.Items);
                    $scope.articles.totalServerItems = data.Count;

                    $scope.$broadcast("scroll.infiniteScrollComplete");
                }, function (data) {
                    showError(data);
                });
        };

        $scope.articles.getPagedData = function (force) {
            return articlesService.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, "", force)
                .then(function (data) {
                    $scope.articles.articlesData = data.Items;
                    $scope.articles.totalServerItems = data.Count;
                }, function (data) {
                    showError(data);
                });
        };

        ttTools.logger.info("Loading articles...");


        $scope.articles.getPagedData(articlesService.toBeForced);

        $scope.$watch("articles.pagingOptions", function (newVal, oldVal) {
            if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                if (newVal.currentPage < 1) {
                    $scope.articles.pagingOptions.currentPage = 1;
                } else {
                    $scope.articles.getPagedData();
                }
            }
        }, true);

        $scope.$on(signalRSubscribe + "articleChange", function () {
            $scope.articles.pagingOptions.moreCurrentPage = 1;
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
            articlesService.deleteArticle(id)
                .success(function () {
                    $scope.articles.getPagedData(true);
                    $scope.articles.selectedFilteredArticle = null;

                    toastService.pop({
                        title: $translate.instant("POPUP_SUCCESS"),
                        body: $translate.instant("POPUP_DELETED"),
                        type: "info"
                    });
                })
                .error(function (data, status, headers, config) {
                    ttTools.logger.error("Server error", data);
                    showError(data);
                });
        };

        function showError(data) {
            dialog.showModalDialog({}, {
                headerText: $translate.instant("COMMON_ERROR"),
                bodyText: $translate.instant("DETAILS_ERROR"),
                closeButtonText: $translate.instant("COMMON_CLOSE"),
                actionButtonText: $translate.instant("COMMON_OK"),
                detailsText: JSON.stringify(data)
            });
        };
    };

    app.module.lazy.controller("articlesController",
        ["$scope", "$location", "articlesService", "signalRSubscribe", "toastService", "dialogService", "$translate", "personalizationService", ArticlesController]);
})();
