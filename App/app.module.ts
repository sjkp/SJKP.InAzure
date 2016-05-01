module SJKP.InAzure.Web {
    export var app = angular.module('SJKP.InAzure', ["ngRoute"]);
    
    app.config(function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/search/:term', {
                templateUrl: 'app/views/searchresults.html',
                controller: 'searchResultController',
                resolve: SearchResultController.resolve()
            })
            .otherwise({
                templateUrl: 'app/views/welcome.html',
                controller: 'welcomeController'
            });

        // configure html5 to get links working on jsfiddle
        $locationProvider.html5Mode(true);
    });

    app.run(['$rootScope', function ($rootScope) {

        $rootScope.isLoading = false;
        $rootScope.$on('$routeChangeStart', function () {
            $rootScope.isLoading  = true;
        });
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.isLoading  = false;
        });
        $rootScope.$on('$routeChangeError', function () {
            //catch error
        });

    }]);
}