define(["app"], function (app) {
    app.filter("baseUrl", function () {
        return function (input) {
            if (input) {
                return ttTools.baseUrl + input;
            }
        };
    });
});