(function () {
    /**
     * @param $http
     * @param $q
     * @param $angularCacheFactory
     */
    $app.ArticlesApi = function ($http, $q, $angularCacheFactory) {
        var articlesCache = $angularCacheFactory("articlesCache", {});

        this.toBeForced = false;

        this.getArticlesPaged = function (pageSize, page, searchText, force) {
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
                }).success(function (response) {
                    articlesCache.put(cacheKey, response);
                    deferred.resolve(response);
                }).error(function (response) {
                    deferred.reject(response);
                });
            }

            return deferred.promise;
        };

        this.dataChanged = function () {
            articlesCache.removeAll();
            this.toBeForced = true;
        };

        this.getArticleDetails = function (id) {
            return $http({
                method: "GET",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        };

        this.saveArticleWithImage = function (artikel, image) {
            return $http({
                method: "POST",
                url: ttTools.baseUrl + "api/articles",
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
        };

        this.deleteArticle = function (id) {
            return $http({
                method: "DELETE",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        };
    };

    app.service("articlesApi", ["$http", "$q", "$angularCacheFactory", $app.ArticlesApi]);
})();
