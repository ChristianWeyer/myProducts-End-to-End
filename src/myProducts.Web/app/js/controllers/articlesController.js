app.lazy.controller("ArticlesController",
    ["$scope", "$location", "articlesApi", "subscribePrefix", "toast", "dialog", "$translate", "personalization",
        function ($scope, $location, articlesApi, subscribePrefix, toast, dialog, $translate, personalization) {
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
                    $scope.articles.getPagedData();
                }
            }, true);

            $scope.$on(subscribePrefix + "articleChange", function () {
                $scope.articles.getPagedData(true);
            });

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
        }]);