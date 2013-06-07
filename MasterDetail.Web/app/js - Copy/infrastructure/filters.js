myApp.filter("baseUrl", function () {
    return function (input) {
        return ttTools.baseUrl + input;
    };
});