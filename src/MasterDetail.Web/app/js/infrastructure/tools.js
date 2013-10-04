var ttTools = ttTools || {};

ttTools.cloudUrl = "https://ngmd.azurewebsites.net/";

ttTools.isInApp = function () {
    var local = document.URL.indexOf("http://") === -1 &&
        document.URL.indexOf("https://") === -1;

    return local;
};

ttTools.getBaseUrl = function () {
    if (ttTools.isInApp()) {
        return ttTools.cloudUrl;
    }
    else {
        return window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split("/")[1] + "/";
    }
};

ttTools.baseUrl = ttTools.getBaseUrl();

ttTools.iOS = function () {
    return (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);
};

ttTools.lowercaseFirstLetter = function(string) {
    return string.charAt(0).toLowerCase() + string.slice(1);
};


ttTools.initLogger = function (url) {
    ttTools.JsonAppender.prototype = new log4javascript.Appender();
    ttTools.JsonAppender.prototype.toString = function () {
        return 'JsonAppender';
    };
    log4javascript.JsonAppender = ttTools.JsonAppender;

    ttTools.logger = log4javascript.getLogger();

    var ajaxAppender = new log4javascript.JsonAppender(url);
    ajaxAppender.setThreshold(log4javascript.Level.INFO);
    ttTools.logger.addAppender(ajaxAppender);

    var consoleAppender = new log4javascript.BrowserConsoleAppender();
    var patternLayout = new log4javascript.PatternLayout("%d{HH:mm:ss,SSS} %-5p - %m{1}%n");
    consoleAppender.setLayout(patternLayout);
    ttTools.logger.addAppender(consoleAppender);
};

ttTools.JsonAppender = function (url) {
    var isSupported = true;
    var successCallback = function (data, textStatus, jqXHR) { return; };

    if (!url) {
        isSupported = false;
    }

    this.setSuccessCallback = function (successCallbackParam) {
        successCallback = successCallbackParam;
    };

    this.append = function (loggingEvent) {
        if (!isSupported) {
            return;
        }

        $.ajax({
            url: url,
            type: "POST",
            dataType: "JSON",
            contentType: "application/json",
            data: JSON.stringify({
                'logger': loggingEvent.logger.name,
                'timestamp': loggingEvent.timeStampInMilliseconds,
                'level': loggingEvent.level.name,
                'url': window.location.href,
                'message': loggingEvent.getCombinedMessages(),
                'exception': loggingEvent.getThrowableStrRep()
            })
        });
    };
};