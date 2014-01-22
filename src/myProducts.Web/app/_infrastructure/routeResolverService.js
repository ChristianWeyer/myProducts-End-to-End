(function () {
    /**
     */
    var RouteResolver = function () {
        this.$get = function () {
            return this;
        };

        this.routeConfig = function () {
            var viewsDirectory = "app/",
                controllersDirectory = "app/",

                setBaseDirectories = function (viewsDir, controllersDir) {
                    viewsDirectory = viewsDir;
                    controllersDirectory = controllersDir;
                },

                getViewsDirectory = function () {
                    return viewsDirectory;
                },

                getControllersDirectory = function () {
                    return controllersDirectory;
                };

            return {
                setBaseDirectories: setBaseDirectories,
                getControllersDirectory: getControllersDirectory,
                getViewsDirectory: getViewsDirectory
            };
        }();

        this.route = function (routeConfig) {
            var resolve = function (baseName, path) {
                if (!path) path = "";
                var lowercaseBaseName = ttTools.lowercaseFirstLetter(baseName);

                var routeDef = {};
                routeDef.templateUrl = routeConfig.getViewsDirectory() + lowercaseBaseName + "/" + lowercaseBaseName + ".html";
                routeDef.controller = baseName + "Controller";
                routeDef.resolve = {
                    load: [
                        "$q", "$rootScope", function ($q, $rootScope) {
                            var dependencies = [routeConfig.getControllersDirectory() + lowercaseBaseName + "/" + lowercaseBaseName + "Controller.js"];
                            return resolveDependencies($q, $rootScope, dependencies);
                        }
                    ]
                };

                return routeDef;
            },

                resolveDependencies = function ($q, $rootScope, dependencies) {
                    var defer = $q.defer();

                    $script(dependencies, function () {
                        $rootScope.$apply(function () {
                            defer.resolve();
                        });
                    });

                    return defer.promise;
                };

            return {
                resolve: resolve
            };
        }(this.routeConfig);
    };

    angular.module("routeResolverServices", []).provider("routeResolver", RouteResolver);
})();
