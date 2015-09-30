(function () {
    'use strict';

    /**
     * @param $rootScope
     * @param $scope
     * @param $timeout
     * @param $http
     * @param $log
     * @param $modal
     * @param $window
     * @param $state
     */
    function AppController($rootScope, $scope, $timeout, $http, $log, $modal, $window) {
        var currentAppVersion;
        var isReloadModalShown;

        function showReloadModal(version) {
            if (isReloadModalShown) {
                return;
            }

            isReloadModalShown = true;

            $log.info('New version detected:', version);

            $modal.open({
                backdrop: 'static',
                keyboard: false,
                scope: $scope,
                template: '<div class="modal-header">' +
                '<h3 class="modal-title">Bitte Seite neu laden!</h3>' +
                '</div>' +
                '<div class="modal-body">' +
                '<p ng-bind="text"></p>' +
                'Es liegt eine neue Version der Anwendung vor. Bitte laden Sie die Seite neu.' +
                '</div>' +
                '<div class="modal-footer">' +
                '<button class="btn btn-primary" ng-click="reloadApplication()" tt-enter="reloadApplication()">Seite neu laden</button>' +
                '</div>'
            });
        }

        function checkAppVersion() {
            getAppVersion()
                .then(function (version) {
                    if (currentAppVersion && version > currentAppVersion) {
                        showReloadModal(version);
                    }

                    currentAppVersion = version;
                })
                .finally(function () {
                    return $timeout(checkAppVersion, 2000);
                });
        }

        function getAppVersion() {
            return $http.get(ttTools.baseUrl + "api/app/getversion")
                .then(function (response) {
                    return response.data;
                });
        }

        checkAppVersion();
    }

    app.module.controller('appController', AppController);
})();
