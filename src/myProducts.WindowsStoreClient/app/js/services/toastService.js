(function () {
    /**
     * @param $rootScope
     */
    $app.Toast = function ($rootScope) {
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

    app.service("toast", ["$rootScope", $app.Toast]);
})();
