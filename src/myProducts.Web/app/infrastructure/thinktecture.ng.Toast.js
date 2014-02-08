var tt = window.tt || {}; tt.toast = {};
tt.toast.module = angular.module("Thinktecture.Toast", ["ng"]);

(function () {
    /**
     * @param $rootScope
     */
    var toast = function ($rootScope) {
        var queue = [], currentMessage = {};
        toastr.options.timeOut = 2000;

        $rootScope.$on("$routeChangeSuccess", function () {
            if (queue.length > 0)
                currentMessage = queue.shift();
            else
                currentMessage = {};
        });

        this.set = function (message) {
            var msg = message;
            queue.push(msg);
        };

        this.get = function (message) {
            return currentMessage;
        };

        this.pop = function (message) {
            switch (message.type) {
                case "success":
                    toastr.success(message.body, message.title);
                    break;
                case "info":
                    toastr.info(message.body, message.title);
                    break;
                case "warning":
                    toastr.warning(message.body, message.title);
                    break;
                case "error":
                    toastr.error(message.body, message.title);
                    break;
            }
        };
    };

    tt.toast.module.service("toast", ["$rootScope", toast]);
})();
