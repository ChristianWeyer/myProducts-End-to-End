app.lazy.controller("ArticleDetailsController",
    ["$scope", "$routeParams", "articlesApi", "toast", "$location", "dialog", "$translate",
        function ($scope, $routeParams, articlesApi, toast, $location, dialog, $translate) {

            if ($routeParams.id !== "new") {
                ttTools.logger.info("Getting article details...");

                $scope.editMode = true;

                articlesApi.getArticleDetails($routeParams.id)
                    .success(function (data) {
                        $scope.artikel = data;
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

            $scope.save = function () {
                var file = null;
                if ($scope.image) file = $scope.image.file;
                
                articlesApi.saveArticleWithImage($scope.artikel, file)
                    .success(function () {
                        toast.pop({
                            title: $translate("POPUP_SUCCESS"),
                            body: $translate("POPUP_SAVED"),
                            type: "success"
                        });
                        $location.path('/articles');
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