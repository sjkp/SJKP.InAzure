module SJKP.InAzure.Web {
    interface IBlogPost {
        content: string;
        date: string;
        excerpt: string;
        title: string;
        permalink: string;
    }

    export interface IBlogControllerScope extends ng.IScope {
        posts: IBlogPost[];
    }

    export class BlogController {
        static $inject = ["$scope", "$http"]

        constructor(private $scope: IBlogControllerScope, private $http: ng.IHttpService) {
            var url = "http://wp.sjkp.dk/?feed=json&jsonp=JSON_CALLBACK"

            $http.jsonp(url).then(res => {
                this.$scope.posts = (<IBlogPost[]>res.data).slice(0,4);
            });
        }


    }

    app.controller('blogController', BlogController);
}