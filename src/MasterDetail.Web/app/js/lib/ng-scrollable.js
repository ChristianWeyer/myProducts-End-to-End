'use strict';

angular.module('ng-scrollable', []).directive('ngScrollable', function () {
    return {
        restrict: 'A',
        replace: false,
        link: function (scope, element, attrs) {
            document.addEventListener('touchmove', function (e) {
                e.preventDefault();
            }, false);

            window.addEventListener("load", enableScroll, false);

            scope.$on('refreshScroll', function () {
                refreshScroll();
            });

            var myScroll;

            function enableScroll() {
                myScroll = new iScroll(element[0], {
                    hScroll: true,
                    vScroll: true,
                    hScrollbar: true,
                    vScrollbar: true,
                    fixedScrollbar: true,
                    fadeScrollbar: true,
                    hideScrollbar: false,
                    bounce: false,
                    momentum: true,
                    lockDirection: true
                });
            }

            function refreshScroll() {
                myScroll.refresh();
            }
        },
    };
});