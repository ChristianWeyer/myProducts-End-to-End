myApp.controller("ArticlesController",
    ["$scope", "$location", "articlesApiService", "pushService", function ($scope, $location, articlesApiService, pushService) {

    articlesApiService.ping(); // needed to trigger authN (for now...)
    $scope.articles = articlesApiService.getArticleList();
    
    pushService.on("articleChanged", function () {
        $scope.bestandsArtikelListe.read();
    });
    
    $scope.rowSelected = function (data) {
        $location.path("/details/" + data.Id);
    };
}]);
