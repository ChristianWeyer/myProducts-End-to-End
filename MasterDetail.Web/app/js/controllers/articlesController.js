myApp.controller("ArticlesController",
    ["$scope", "$location", "articlesApiService", "pushService", function ($scope, $location, articlesApiService, pushService) {

    $scope.articles = articlesApiService.getArticleList();
    
    pushService.on("articleChanged", function () {
        $scope.articles.read();
    });
    
    $scope.rowSelected = function (data) {
        $location.path("/details/" + data.Id);
    };
}]);
