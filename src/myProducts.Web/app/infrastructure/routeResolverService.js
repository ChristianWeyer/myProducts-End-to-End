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
            var resolve = function (moduleConfig /*baseName, path*/) {
                if (!moduleConfig.Path) moduleConfig.Path = "";
                var lowercaseBaseName = ttTools.lowercaseFirstLetter(moduleConfig.Module);

                var routeDef = {};
                routeDef.url = moduleConfig.Url,
                routeDef.templateUrl = routeConfig.getViewsDirectory() + lowercaseBaseName + "/" + lowercaseBaseName + ".html";
                //routeDef.controller = moduleConfig.Module + "Controller";
                routeDef.controllerProvider = function ($stateParams) {
                    return moduleConfig.Module + "Controller";
                };
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
