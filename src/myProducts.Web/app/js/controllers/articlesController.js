app.lazy.controller("ArticlesController",
    ["$scope", "$location", "articlesApi", "dataPush", "toast", "dialog", "$translate", "personalization", "settings",
        function ($scope, $location, articlesApi, dataPush, toast, dialog, $translate, personalization, settings) {
            $scope.pagingOptions = { pageSizes: [10], pageSize: 10, currentPage: 1 };
            $scope.filterOptions = {
                filterText: "",
                useExternalFilter: true
            };
            $scope.gridOptions = {
                data: "articlesData",
                enablePaging: true,
                showFooter: true,
                totalServerItems: "totalServerItems",
                pagingOptions: $scope.pagingOptions,
                filterOptions: $scope.filterOptions,
                showFilter: true,
                multiSelect: false,
                columnDefs: [{ field: 'Name', displayName: 'Name' }, { field: 'Code', displayName: 'Code' }, { cellTemplate: "app/views/gridCellTemplate.html", width: "90px" }]
            };

            if (settings.enablePdfExport) {
                $scope.gridOptions.plugins = [new ngGridPdfExportPlugin({})];
            }

            $scope.getFilteredData = function (searchText) {
                var search = searchText;
                if ($scope.gridOptions.$gridScope.filterText) search = $scope.gridOptions.$gridScope.filterText;

                return articlesApi.getArticlesPaged($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, search, false)
                    .then(function (data) {
                        $scope.articlesData = data.Items;
                        $scope.totalServerItems = data.Count;

                        return data.Items;
                    });
            };

            $scope.getPagedData = function (force) {
                return articlesApi.getArticlesPaged($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, "", force)
                    .then(function (data) {
                        $scope.articlesData = data.Items;
                        $scope.totalServerItems = data.Count;
                    });
            };

            ttTools.logger.info("Loading articles...");
            $scope.getPagedData(true);

            $scope.$watch("gridOptions.$gridScope.filterText", function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    $scope.getFilteredData();
                }
            }, true);

            $scope.$watch("pagingOptions", function (newVal, oldVal) {
                if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                    $scope.getPagedData();
                }
            }, true);

            $scope.capabilities = personalization.data.UiClaims.Capabilities;
            $scope.capabilities.has = function (key) {
                return $scope.capabilities.indexOf(key) > -1;
            };

            $scope.$on(tt.signalr.subscribe + "articleChange", function () {
                $scope.getPagedData(true);
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
                        $scope.getPagedData(true);

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