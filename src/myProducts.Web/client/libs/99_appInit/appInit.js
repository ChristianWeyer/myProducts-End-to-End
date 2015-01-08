(function () {
    "use strict";

    window.app = window.app || { resolver: {} };

    if (ttMobile) {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "ngStorage", "chart.js", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    } else {
        app.module = angular.module("myApp", ["myApp.embeddedTemplates", "ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "mgcrea.ngStrap", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "chart.js", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    }

    app.module.config(function ($compileProvider, $translateProvider, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
        $compileProvider.debugInfoEnabled(false);
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);

        FastClick.attach(document.body);

        cfpLoadingBarProvider.includeSpinner = false;

        tokenAuthenticationServiceProvider.setUrl(ttTools.baseUrl + "token");

        ttTools.initLogger(ttTools.baseUrl + "api/log");
        ttTools.logger.info("Configuring myApp...");

        $translateProvider.translations("de", tt.translations.de);
        $translateProvider.useStaticFilesLoader({
            prefix: "translations/locale-",
            suffix: ".json"
        });
        $translateProvider.preferredLanguage("de");
        $translateProvider.useLocalStorage();
    });
})();

window.tt = window.tt || {}; tt.translations = {};
tt.translations.de = {
    "APP_UPDATE_BODY": "Neue Version vorhanden - jetzt laden?",

    "LOGIN_TITLE": "Anmeldung",
    "LOGIN_USERNAME": "Benutzer",
    "LOGIN_PASSWORD": "Passwort",
    "LOGIN_BUTTON_LOGIN": "Anmelden",
    "LOGIN_SUCCESS": "Anmeldung erfolgreich.",
    "LOGIN_FAILED": "Anmeldung fehlgeschlagen.",

    "INDEX_TITLE": "meineProdukte",
    "INDEX_ARTICLES": "Artikel",
    "INDEX_GALLERY": "Galerie",
    "INDEX_INFO": "Über",
    "INDEX_SETTINGS": "Einstellungen",
    "INDEX_LOGS": "Logs",
    "INDEX_STATS": "Statistiken",
    "INDEX_LOGIN": "Anmelden",
    "INDEX_LOGOUT": "[Abmelden]",
    "INDEX_USERNAME": "Benutzer",

    "OVERVIEW_TITLE": "Überblick",
    "OVERVIEW_FILTER": "Artikel-Name-Filterung",
    "OVERVIEW_SEARCH_PLACEHOLDER": "Nach Artikeln suchen...",
    "OVERVIEW_SEARCH_BUTTON": "Suchen",

    "GALLERY_TITLE": "Produktbilder",
    "GALLERY_BODY": "Wischen Sie durch die Bilder.",

    "DETAILS_TITLE": "Artikel-Details",
    "DETAILS_SELECTIMAGE": "Passendes Bild...",
    "DETAILS_ERROR": "Artikeldaten konnten nicht geladen werden (siehe Details).",
    "DETAILS_SAVE_ERROR": "Artikeldaten konnten nicht gespeichert werden (siehe Details).",
    "DETAILS_NAME": "Name",
    "DETAILS_CODE": "Code",
    "DETAILS_DESCRIPTION": "Beschreibung",
    "DETAILS_CATEGORIES": "Kategorien",

    "ABOUT_TITLE": "Über diese Anwendung",
    "ABOUT_BODY": "Eine auf HTML5-basierende Produktverwaltung.",
    "ABOUT_CONTACT": "Für Fragen & Feedback: <a href='mailto:christian.weyer@thinktecture.com'>Email an Christian Weyer</a>",

    "LOGS_TITLE": "Log-Daten (über Server)",

    "STATS_TITLE": "Statistiken",

    "SETTINGS_TITLE": "Einstellungen",
    "SETTINGS_YES": "Ja",
    "SETTINGS_NO": "Nein",
    "SETTINGS_PUSH": "Push-Kommunikation",
    "SETTINGS_PDFEXPORT": "PDF-Export (sehr einfach; experimentell)",
    "SETTINGS_SENDPOS": "Position übermitteln",

    "POPUP_SUCCESS": "Erfolg",
    "POPUP_ERROR": "Fehler",
    "POPUP_SAVED": "Gespeichert.",
    "POPUP_DELETED": "Gelöscht.",

    "COMMON_YES": "Ja",
    "COMMON_NO": "Nein",
    "COMMON_OK": "OK",
    "COMMON_CLOSE": "Schliessen",
    "COMMON_ERROR": "Fehler",
    "COMMON_INFO": "Information",
    "COMMON_CANCEL": "Abbrechen",
    "COMMON_SAVE": "Speichern",
    "COMMON_LANG": "Sprache",
    "COMMON_DE": "Deutsch",
    "COMMON_EN": "Englisch"
}