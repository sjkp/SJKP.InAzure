module SJKP.InAzure.Web {
    interface ISearchControllerScope extends ng.IScope {
        search: Function;
        searchTerm: string;
        latestSearchTerms: string[];
    }

    export class SearchController {
        static $inject = ["$scope", "$location", "$http", "$timeout"]

        constructor(private $scope: ISearchControllerScope, private $location: ng.ILocationService, private $http: ng.IHttpService, private $timeout: ng.ITimeoutService) {
            //this.$scope.latestSearchTerms = [];
            this.$http.get('/api/v1/search/history').then(res => {
                this.$timeout(() => {
                    console.log(res.data);
                    this.$scope.latestSearchTerms = <string[]>res.data;
                });
            });
            this.$scope.$on('$routeChangeSuccess', () => {
                var arr = $location.path().split('/');
                if (arr.lastIndexOf('search') + 1 <= arr.length) {
                    this.$scope.searchTerm = arr[arr.lastIndexOf('search') + 1];
                }
            });

            $scope.search = () => {
                console.log('search' + this.$scope.searchTerm);
                $scope.$broadcast('newSearch', this.$scope.searchTerm);
                this.$location.path("search/" + this.$scope.searchTerm);
            }

            $scope.$on('newSearch', (event, args) => {
                this.$scope.latestSearchTerms = [args].concat(this.$scope.latestSearchTerms.slice(0,4));
            });
        }
    }

    app.controller('searchController', SearchController);
}