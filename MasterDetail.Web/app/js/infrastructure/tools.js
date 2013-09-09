var ttTools = ttTools || {};

ttTools.cloudUrl = "https://ngmd.azurewebsites.net/";

ttTools.isInApp = function () {
    var local = document.URL.indexOf("http://") === -1 &&
        document.URL.indexOf("https://") === -1;

    return local;
};

ttTools.getBaseUrl = function () {
    if(ttTools.isInApp()) {
        return ttTools.cloudUrl
    }
    else {
        return window.location.protocol + "//" + window.location.host + "/" + window.location.pathname;
    }
};

ttTools.baseUrl = ttTools.getBaseUrl();
