define(['app'], function (app) {
    app.factory("articlesApiService", ["$http", function ($http) {
        var service = {
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
                            return data.Items;
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
                    method: "POST",
                    url: ttTools.baseUrl + "api/articles/" + artikel.Id,
                    data: artikel
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
});