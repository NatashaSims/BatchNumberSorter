(function (app) {
    'use strict';

    app.controller('runBatchCtrl', runBatchCtrl);

    runBatchCtrl.$inject = ['$scope', '$modalInstance', '$location', 'apiService', 'notificationService'];

    function runBatchCtrl($scope, $modalInstance, $location, apiService, notificationService) {

        $scope.loadDirectionItems = loadDirectionItems;
        $scope.run = { BatchID: $scope.batch.ID, Batch: $scope.batch.BatchNumbers };
        $scope.runBatch = runBatch;
        $scope.cancelRun = cancelRun;
        $scope.directionItems = [];

        function loadDirectionItems() {
            notificationService.displayInfo('Loading available directions');

            apiService.get('/api/directions/all', null,
            DirectionItemsLoadCompleted,
            DirectionItemsLoadFailed);
        }

        function DirectionItemsLoadCompleted(response) {
            $scope.directionItems = response.data;
            $scope.selectedDirection = $scope.directionItems[0].ID;
            console.log(response);
        }

        function DirectionItemsLoadFailed(response) {
            console.log(response);
            notificationService.displayError(response.data);
        }


        function runBatch() {
            $scope.run.Direction = $scope.selectedDirection;
            apiService.post('/api/runs/createrun', $scope.run,
            runBatchSucceeded,
            runBatchFailed);
        }


        function runBatchSucceeded(response) {
            notificationService.displaySuccess('Batch run completed successfully');
            $modalInstance.close();
        }


        function runBatchFailed(response) {
            notificationService.displayError(response.data.Message);
        }


        function cancelRun() {
            $modalInstance.dismiss();
        }

        loadDirectionItems();
    }

})(angular.module('media'));