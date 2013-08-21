myApp.filter("baseUrl", function () {
    return function (input) {
        console.log("***Calculated URL: " + ttTools.baseUrl + input);
        return ttTools.baseUrl + input;
    };
});