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
        if (StatusBar) {
            StatusBar.hide();
        }

        if (navigator.splashscreen) {
            navigator.splashscreen.hide();
        }
    }
};
