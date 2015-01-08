/**
 * Created by christian on 17.01.14.
 */
describe("basic tests", function() {
    it("should be true", function() {
        expect(true).toBe(true);
    });
});

describe("loginController", function () {
    var controller;
    var scope;
    var _tokenAuthentication;

    beforeEach(module("myApp"));

    beforeEach(inject(function ($rootScope, $controller, tokenAuthentication) {
        scope = $rootScope.$new();

        controller = $controller(LoginController, {
            $scope: scope, _tokenAuthentication: tokenAuthentication
        });
    }));

    it("should have a login controller", function () {
        expect($app.LoginController).not.toEqual(null);
    });

    it("should allow a correct user to log in", function () {

    });
});

