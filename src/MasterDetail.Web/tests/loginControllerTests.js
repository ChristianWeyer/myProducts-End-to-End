/// <reference path="jasmine.js" />
/// <reference path="../app/js/lib/angular-.js" />
/// <reference path="angular-mocks.js" />
/// 
/// <reference path="../app/js/lib/" />
/// <reference path="../app/js/infrastructure/" />
/// <reference path="../app/js/services/" />
/// <reference path="../app/js/constants.js" />
/// <reference path="../app/translations/translations-de.js" />
/// <reference path="../app/js/app.js" />
/// <reference path="../app/js/controllers/loginController.js" />

describe("Login controller", function () {
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
    
    it("Should have a LoginController controller", function () {
        expect(app.LoginCtrl).not.to.equal(null);
    });

    it("Correct user can log in", function () {

    });
})