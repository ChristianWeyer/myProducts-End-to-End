var ttTools = ttTools || {};

ttTools.isInApp = function () {
    var local = document.URL.indexOf("http://") === -1 &&
        document.URL.indexOf("https://") === -1;

    return local;
};

ttTools.getBaseUrl = function () {
    if (ttTools.isInApp())
        return window.location.protocol + "//" + window.origin.host + "/";
    else return "../";
};

ttTools.baseUrl = ttTools.getBaseUrl();
