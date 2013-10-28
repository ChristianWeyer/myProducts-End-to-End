app.factory("articlesApi", ["$http", "$q", "$angularCacheFactory", function ($http, $q, $angularCacheFactory) {
    var articlesCache = $angularCacheFactory("articlesCache", {});

    var service = {
        getArticlesPaged: function (pageSize, page, searchText, force) {
            var deferred = $q.defer();
            var cacheKey = "articles_" + pageSize + "_" + page;
            
            if (!force && (!searchText && articlesCache.get(cacheKey))) {
                deferred.resolve(articlesCache.get(cacheKey));
            } else {
                var url = ttTools.baseUrl + "api/articles?$inlinecount=allpages&$top=" + pageSize + "&$skip=" + (page - 1) * pageSize;
                
                if (searchText) {
                    url += "&$filter=substringof('" + searchText.toLowerCase() + "',tolower(Name))";
                }

                $http({
                    method: "GET",
                    url: url
                }).success(function(response) {
                    articlesCache.put(cacheKey, response);
                    deferred.resolve(response);
                }).error(function (response) {
                    deferred.reject(response);
                });
            }

            return deferred.promise;
        },

        dataChanged: function() {
            articlesCache.removeAll();
        },
        
        getArticleDetails: function (id) {
            return $http({
                method: "GET",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        },

        saveArticleWithImage: function (artikel, image) {
            return $http({
                method: "POST",
                url: "api/articles",
                headers: { "Content-Type": undefined },
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