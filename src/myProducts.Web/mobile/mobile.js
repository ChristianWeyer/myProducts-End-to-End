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
        //alert("onDeviceReady");
        if (isPhonegap() && isIOS() && window.device && parseFloat(window.device.version) >= 7.0) {
            $('body').addClass('phonegap-ios-7');
        }
    }
};

function isPhonegap() {
    return typeof cordova !== 'undefined' || typeof PhoneGap !== 'undefined' || typeof phonegap !== 'undefined';
}

function isIOS() {
    return navigator.userAgent.match(/(iPad|iPhone|iPod)/g);
}
