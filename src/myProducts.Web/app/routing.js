(function () {
    "use strict";

    app.module.config(function ($urlRouterProvider, $stateProvider) {
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state("info", getRouteConfiguration("info"))
            .state("settings", getRouteConfiguration("settings"))
            .state("login", getRouteConfiguration("login"))
            .state("start", getRouteConfiguration("start", "/"))
            .state("articles", getRouteConfiguration("articles"))
            .state("articledetails", { url: "/articledetails/:id", templateUrl: "articleDetails/articleDetails.html", controller: "articleDetailsController" })
            .state("gallery", getRouteConfiguration("gallery"))
            .state("log", getRouteConfiguration("log"))
            .state("statistics", getRouteConfiguration("statistics"));

        function getRouteConfiguration(name, overrideUrl) {
            var url = "/" + name;

            if (overrideUrl) {
                url = overrideUrl;
            }

            return {
                url: url,
                templateUrl: name + "/" + name + ".html",
                controller: name + "Controller"
            };
        }
    });
})();
