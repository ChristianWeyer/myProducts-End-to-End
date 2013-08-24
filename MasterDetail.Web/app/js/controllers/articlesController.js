myApp.controller("ArticlesController",
    ["$scope", "$rootScope", "$location", "articlesApiService", "pushService", function ($scope, $rootScope, $location, articlesApiService, pushService) {
        $scope.articles = articlesApiService.getArticleList();
        
        $rootScope.$on(tt.authentication.constants.loggedIn, function () {
            $scope.articles.read();
        });

        pushService.on("articleChanged", function () {
            $scope.articles.read();
        });

        $scope.rowSelected = function (data) {
            $location.path("/details/" + data.Id);
        };
    }]);
