app.factory("articlesApi", ["$http", "$q", "$angularCacheFactory", function ($http, $q, $angularCacheFactory) {
    var dataCache = $angularCacheFactory('dataCache', {
    });

    var service = {
        getArticlesPaged: function (pageSize, page, searchText) {
            var deferred = $q.defer();
            var cacheKey = "articles_" + pageSize + "_" + page;
            
            if (dataCache.get(cacheKey)) {
                deferred.resolve(dataCache.get(cacheKey));
            } else {
                var url = ttTools.baseUrl + "api/articles?$inlinecount=allpages&$top=" + pageSize + "&$skip=" + (page - 1) * pageSize;
                
                if (searchText) {
                    url += "&$filter=substringof('" + searchText.toLowerCase() + "',tolower(Name))";
                }

                $http({
                    method: "GET",
                    url: url
                }).success(function(response) {
                    dataCache.put(cacheKey, response);
                    deferred.resolve(response);
                }).error(function (response) {
                    deferred.reject(response);
                });
            }

            return deferred.promise;
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