myApp.factory("articlesApiService", ["$http", function ($http) {
    var service = {
        ping : function() {
            $http({
                method: "GET",
                url: ttTools.baseUrl + "api/ping"
            });
        },
        
        getArticleList: function () {
            var ds = new kendo.data.DataSource({
                type: "odata",
                transport: {
                    read: {
                        url: ttTools.baseUrl + "api/articles",
                        dataType: "json"
                    }
                },
                serverFiltering: true,
                serverPaging: true,
                pageSize: 10,
                schema: {
                    data: function (data) {
                        return data.Results;
                    },
                    total: function (data) {
                        return data.Count;
                    }
                }
            });

            return ds;
        },

        getArticleDetails: function (id) {
            return $http({
                method: "GET",
                url: ttTools.baseUrl + "api/articles/" + id
            });
        },

        saveArticle: function (artikel) {
            return $http({
                method: "PUT",
                url: ttTools.baseUrl + "api/articles/" + artikel.Id,
                data: artikel
            });
        }
    };

    return service;
}]);
