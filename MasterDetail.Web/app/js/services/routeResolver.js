﻿define([], function () {
    var services = angular.module('routeResolverServices', []);

    services.provider('routeResolver', function () {
        this.$get = function () {
            return this;
        };

        this.routeConfig = function () {
            var viewsDirectory = '/app/views/',
                controllersDirectory = '/app/controllers/',

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
                if (!path) path = '';
                var lowercaseBaseName = lowercaserFirstLetter(baseName);

                var routeDef = {};
                routeDef.templateUrl = routeConfig.getViewsDirectory() + path + lowercaseBaseName + '.html';
                routeDef.controller = baseName + 'Controller';
                routeDef.resolve = {
                    load: ['$q', '$rootScope', function ($q, $rootScope) {
                        var dependencies = [routeConfig.getControllersDirectory() + path + lowercaseBaseName + 'Controller.js'];
                        return resolveDependencies($q, $rootScope, dependencies);
                    }]
                };

                return routeDef;
            },

            resolveDependencies = function ($q, $rootScope, dependencies) {
                var defer = $q.defer();
                require(dependencies, function () {
                    defer.resolve();
                    $rootScope.$apply();
                });

                return defer.promise;
            };

            function lowercaserFirstLetter(string) {
                return string.charAt(0).toLowerCase() + string.slice(1);
            }
            
            return {
                resolve: resolve
            };
        }(this.routeConfig);
    });
});
