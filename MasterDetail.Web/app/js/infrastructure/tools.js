var ttTools = ttTools || {};

ttTools.isInApp = function () {
    var local = document.URL.indexOf("http://") === -1 &&
        document.URL.indexOf("https://") === -1;

    return local;
};

ttTools.getBaseUrl = function () {
    return window.location.protocol + "//" + window.location.host + "/" + window.location.pathname;
};

ttTools.baseUrl = ttTools.getBaseUrl();
