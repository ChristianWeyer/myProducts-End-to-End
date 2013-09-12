myApp.controller("ArticlesController",
    ["$scope", "$rootScope", "$location", "articlesApiService", "dataPushService", function ($scope, $rootScope, $location, articlesApiService, dataPushService) {
        $scope.articles = articlesApiService.getArticleList();
        
        $rootScope.$on(tt.authentication.constants.loggedIn, function () {
            $scope.articles.read();
        });

        dataPushService.on("articleChanged", function () {
            $scope.articles.read();
        });

        $scope.rowSelected = function (data) {
            $location.path("/details/" + data.Id);
        };
    }]);
