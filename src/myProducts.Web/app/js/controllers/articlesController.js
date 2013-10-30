app.lazy.controller("ArticlesController",
    ["$scope", "$location", "articlesApi", "dataPush", "toast", "dialog", "$translate", "personalization", "settings",
        function ($scope, $location, articlesApi, dataPush, toast, dialog, $translate, personalization, settings) {
            $scope.capabilities = personalization.data.UiClaims.Capabilities;
            $scope.capabilities.has = function (key) {
                return $scope.capabilities.indexOf(key) > -1;
            };

            $scope.articles = {};
            $scope.articles.pagingOptions = { pageSizes: [10], pageSize: 10, currentPage: 1 };
            $scope.articles.filterOptions = {
                filterText: "",
                useExternalFilter: true
            };
            $scope.articles.gridOptions = {
                data: "articles.articlesData",
                enablePaging: true,
                showFooter: true,
                totalServerItems: "articles.totalServerItems",
                pagingOptions: $scope.articles.pagingOptions,
                filterOptions: $scope.articles.filterOptions,
                showFilter: true,
                multiSelect: false,
                columnDefs: [{ field: 'Name', displayName: 'Name' }, { field: 'Code', displayName: 'Code' }, { cellTemplate: "app/views/gridCellTemplate.html", width: "90px" }]
            };

            if (settings.enablePdfExport) {
                $scope.articles.gridOptions.plugins = [new ngGridPdfExportPlugin({})];
            }

            $scope.articles.getFilteredData = function (searchText) {
                var search = $scope.articles.gridOptions.$gridScope.filterText ? $scope.articles.gridOptions.$gridScope.filterText : searchText;

                return articlesApi.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, search, false)
                    .then(function (data) {
                        $scope.articles.articlesData = data.Items;
                        $scope.articles.totalServerItems = data.Count;

                        return data.Items;
                    });
            };

            $scope.articles.getPagedData = function (force) {
                return articlesApi.getArticlesPaged($scope.articles.pagingOptions.pageSize, $scope.articles.pagingOptions.currentPage, "", force)
                    .then(function (data) {
                        $scope.articles.articlesData = data.Items;
                        $scope.articles.totalServerItems = data.Count;
                    });
            };

            ttTools.logger.info("Loading articles...");
            $scope.articles.getPagedData(false);

            $scope.$watch("articles.gridOptions.$gridScope.filterText", function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    $scope.articles.getFilteredData();
                }
            }, true);

            $scope.$watch("articles.pagingOptions", function (newVal, oldVal) {
                if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                    $scope.articles.getPagedData();
                }
            }, true);

            $scope.$on(tt.signalr.subscribe + "articleChange", function () {
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

                        toast.pop({
                            title: $translate("POPUP_SUCCESS"),
                            body: $translate("POPUP_DELETED"),
                            type: "success"
                        });
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
            };
        }]);