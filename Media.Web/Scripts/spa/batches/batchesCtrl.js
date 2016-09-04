(function (app) {
    'use strict';

    app.controller('batchesCtrl', batchesCtrl);

    batchesCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function batchesCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-batches';
        $scope.loadingBatches = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        
        $scope.Batches = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        function search(page) {
            page = page || 0;

            $scope.loadingBatches = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filterBatches
                }
            };

            apiService.get('/api/batches/', config,
            batchesLoadCompleted,
            batchesLoadFailed);
        }

        function batchesLoadCompleted(result) {
            $scope.Batches = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingBatches = false;

            if ($scope.filterBatches && $scope.filterBatches.length)
            {
                notificationService.displayInfo(result.data.Items.length + ' batches found');
            }
            
        }

        function batchesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterBatches = '';
            search();
        }

        $scope.search();
    }

})(angular.module('media'));