module SJKP.InAzure.Web {
    export class WelcomeController {
        static $inject = ["$scope"];
        constructor(private $scope: ng.IScope) {
        }

    }

    app.controller('welcomeController', WelcomeController);
}