myApp.controller("ArticlesController",
    ["$scope", "$location", "articlesApiService", "pushService", function ($scope, $location, articlesApiService, pushService) {
        $rootScope.$on(tt.authentication.constants.loggedIn, function () {
            $scope.articles = articlesApiService.getArticleList();
        });

        pushService.on("articleChanged", function () {
            $scope.articles.read();
        });

        $scope.rowSelected = function (data) {
            $location.path("/details/" + data.Id);
        };
    }]);
