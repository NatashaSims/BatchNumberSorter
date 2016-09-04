(function () {
    'use strict';

    angular.module('media', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/batches", {
                templateUrl: "scripts/spa/batches/batches.html",
                controller: "batchesCtrl"
            })
            .when("/batches/add", {
                templateUrl: "scripts/spa/batches/add.html",
                controller: "batchAddCtrl"
            })
            .when("/batches/:id", {
                templateUrl: "scripts/spa/batches/details.html",
                controller: "batchDetailsCtrl"                      
            })
            .otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
       
        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

})();