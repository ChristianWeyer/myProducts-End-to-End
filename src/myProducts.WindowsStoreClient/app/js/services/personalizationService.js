(function () {
    /**
     * @param $localStorage
     */
    $app.Personalization = function ($localStorage) {
        var lsPersonalization = $localStorage.personalization = $localStorage.personalization || {};

        return lsPersonalization;
    };

    app.factory("personalization", ["$localStorage", $app.Personalization]);
})();
