var mobileApp = {
    initialize: function () {
        if (window.cordova) {
            this.bindEvents();
        }
    },

    bindEvents: function () {
        document.addEventListener("deviceready", this.onDeviceReady, false);
    },

    onDeviceReady: function () {
        StatusBar.hide();
        navigator.splashscreen.hide();
    }
};
