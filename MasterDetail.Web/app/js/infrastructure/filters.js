myApp.filter("baseUrl", function () {
    return function (input) {
        if (input) {
            return ttTools.baseUrl + input;
        }
    };
});