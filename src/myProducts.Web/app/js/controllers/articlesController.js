app.register.controller("ArticlesController",
    ["$scope", "$location", "articlesApi", "dataPush", "toast", "dialog", "$translate", "personalization",
        function ($scope, $location, articlesApi, dataPush, toast, dialog, $translate, personalization) {
            $scope.totalServerItems = 0;
            $scope.pagingOptions = { pageSizes: [10], pageSize: 10, currentPage: 1 };
            $scope.gridOptions = {
                data: "articlesData",
                enablePaging: true,
                showFooter: true,
                totalServerItems: "totalServerItems",
                pagingOptions: $scope.pagingOptions,
                columnDefs: [{ field: 'Name', displayName: 'Name' }, { field: 'Code', displayName: 'Code' }, { cellTemplate: "<i class=\"btn icon-edit\" ng-click=\"getArticleDetails(&apos;#=Id#&apos;)\"></i><i class=\"btn icon-trash\" ng-click=\"deleteArticle(&apos;#=Id#&apos;)\"></i>" }]
            };

            $scope.getPagedData = function (pageSize, page) {
                articlesApi.getArticlesPaged(pageSize, page).success(function (data) {
                    $scope.articlesData = data.Items;
                    $scope.totalServerItems = data.Count;
                });
            };

            ttTools.logger.info("Loading articles...");
            $scope.getPagedData($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);
            
            $scope.$watch("pagingOptions", function (newVal, oldVal) {
                if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                    $scope.getPagedData($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);
                }
            }, true);
 
            $scope.capabilities = personalization.data.UiClaims.Capabilities;
            $scope.capabilities.has = function (key) {
                return $scope.capabilities.indexOf(key) > -1;
            };

            $scope.$on(tt.signalr.subscribe + "articleChange", function () {
                $scope.getPagedData($scope.pagingOptions.pageSize, 0);
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