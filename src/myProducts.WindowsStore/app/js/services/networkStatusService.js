app.factory("networkStatus", function () {
    return {
        isOnline: function () {
            return navigator.onLine;
        }
    };
});