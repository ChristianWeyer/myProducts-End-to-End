(function () {
    /**
     */
    var RouteResolver = function () {
        this.$get = function () {
            return this;
        };

        this.routeConfig = function () {
            var baseDir = "app/";

            setDirectory = function (dir) {
                baseDir = dir;
            },
            getDirectory = function () {
                return baseDir;
            };

            return {
                setDirectory: setDirectory,
                getDirectory: getDirectory,
            };
        }();

        this.route = function (routeConfig) {
            var resolve = function (baseName, path) {
                if (!path) path = "";
                var lowercaseBaseName = ttTools.lowercaseFirstLetter(baseName);

                var routeDef = {};
                routeDef.templateUrl = routeConfig.getDirectory() + lowercaseBaseName + "/" + lowercaseBaseName + ".html";
                routeDef.controller = baseName + "Controller";
                routeDef.resolve = {
                    load: [
                        "$q", "$rootScope", function ($q, $rootScope) {
                            var dependencies = [routeConfig.getDirectory() + lowercaseBaseName + "/" + lowercaseBaseName + "Controller.js"];
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
