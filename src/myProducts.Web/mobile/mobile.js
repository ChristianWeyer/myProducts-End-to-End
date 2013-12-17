var ttMobile = true;

var mobileApp = {
    initialize: function () {
        if (!document.URL.match(/^https?:/)) {
            this.bindEvents();
        }
    },

    bindEvents: function () {
        document.addEventListener("load", this.onLoad, false);
        document.addEventListener("deviceready", this.onDeviceReady, false);
        window.addEventListener("orientationchange", orientationChange, true);
    },

    onLoad: function () {
    },

    onDeviceReady: function () {
    }
};
