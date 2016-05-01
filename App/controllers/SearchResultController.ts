module SJKP.InAzure.Web {
    interface ISearchResultData {
        MatchedSearchTerm: string;
        Region: string;
    }

    interface ISearchResultControllerScope extends ng.IScope {
        data: ISearchResultData;
    }


    export class SearchResultController {
        static $inject = ["$scope", "searchResultData"];
        constructor(private $scope: ISearchResultControllerScope, searchResultData: ISearchResultData) {
            this.$scope.data = searchResultData;
            console.log(searchResultData);
        }

        public static resolve = () => {
            return {
                // I will cause a 1 second delay
                searchResultData: function ($q: ng.IQService, $http: ng.IHttpService, $route) {
                    var searchTerm = $route.current.params.term;
                    console.log(searchTerm);
                    
                    var delay = $q.defer();
                    $http.post('/api/v1/search', {
                        Term: searchTerm
                    }).then(res => {
                            delay.resolve(res.data);
                        }).catch(err => {
                        delay.reject(err);
                    });

                    return delay.promise;
                }
            };
        }
    }

    app.controller('searchResultController', SearchResultController);
}