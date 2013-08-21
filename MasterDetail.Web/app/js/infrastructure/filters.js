myApp.filter("baseUrl", function () {
    return function (input) {
        if (input) {
            console.log("***Calculated URL: " + ttTools.baseUrl + input);
            return ttTools.baseUrl + input;
        }
    };
});