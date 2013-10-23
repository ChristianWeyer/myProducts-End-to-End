app.factory("articlesApi", ["$http", function ($http) {
    var service = {
        getArticlesPaged: function (pageSize, page, searchText) {
            return $http({
                method: "GET",
                url: "api/articles?$inlinecount=allpages&$top=" + pageSize + "&$skip=" + (page - 1) * pageSize + "&filter=substringof('" + searchText + "',Name)"
            });
        },

        getArticleDetails: function (id) {
            return $http({
                method: "GET",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        },

        saveArticle: function (artikel) {
            return $http({
                method: "POST",
                url: ttTools.baseUrl + "api/articles/" + artikel.Id,
                data: artikel
            });
        },

        saveArticleWithImage: function (artikel, image) {
            return $http({
                method: "POST",
                url: "api/articles",
                headers: { "Content-Type": false },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("model", angular.toJson(data.model));

                    if (data.file) {
                        formData.append("file", data.file);
                    }

                    return formData;
                },
                data: { model: artikel, file: image }
            });
        },

        deleteArticle: function (id) {
            return $http({
                method: "DELETE",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        }
    };

    return service;
}]);