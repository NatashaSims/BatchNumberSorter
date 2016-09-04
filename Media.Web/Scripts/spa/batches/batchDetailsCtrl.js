(function (app) {
    'use strict';

    app.controller('batchDetailsCtrl', batchDetailsCtrl);

    batchDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function batchDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-batches';
        $scope.batch = {};
        $scope.loadingBatch = true;
        $scope.loadingRuns = true;
        $scope.isReadOnly = true;
        $scope.runHistory = [];
        $scope.openRunDialog = openRunDialog;
        $scope.clearSearch = clearSearch;

        function loadBatch() {

            $scope.loadingBatch = true;

            apiService.get('/api/batches/' + $routeParams.id, null,
            batchLoadCompleted,
            batchLoadFailed);
        }

        function loadRunHistory() {
            $scope.loadingRuns = true;

            apiService.get('/api/runs/' + $routeParams.id, null,
            runHistoryLoadCompleted,
            runHistoryLoadFailed);
        }

        function loadBatchDetails() {
            loadBatch();
            loadRunHistory();
        }

        function clearSearch()
        {
            $scope.filterRuns = '';
        }

        function batchLoadCompleted(result) {
            $scope.batch = result.data;
            $scope.loadingBatch = false;
        }

        function batchLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function runHistoryLoadCompleted(result) {
            console.log(result);
            $scope.runHistory = result.data;
            $scope.loadingRuns = false;
        }

        function runHistoryLoadFailed(response) {
            notificationService.displayError(response);
        }

        function openRunDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/runs/runBatchModal.html',
                controller: 'runBatchCtrl',
                scope: $scope
            }).result.then(function ($scope) { loadBatchDetails(); }, function () { });
        }

        loadBatchDetails();
    }

})(angular.module('media'));