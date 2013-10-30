app.lazy.controller("ArticleDetailsController",
    ["$scope", "$routeParams", "articlesApi", "toast", "$location", "dialog", "$translate",
        function ($scope, $routeParams, articlesApi, toast, $location, dialog, $translate) {
            $scope.articedetails = {};

            if ($routeParams.id !== "new") {
                ttTools.logger.info("Getting article details...");

                $scope.articedetails.editMode = true;

                articlesApi.getArticleDetails($routeParams.id)
                    .success(function (data) {
                        $scope.articedetails.article = data;
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

            $scope.articedetails.save = function () {
                var file = null;
                if ($scope.articedetails.image) file = $scope.articedetails.image.file;
                
                articlesApi.saveArticleWithImage($scope.articedetails.article, file)
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