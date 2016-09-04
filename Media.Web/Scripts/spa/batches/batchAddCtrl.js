(function (app) {
    'use strict';

    app.controller('batchAddCtrl', batchAddCtrl);

    batchAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService'];

    function batchAddCtrl($scope, $location, $routeParams, apiService, notificationService) {

        $scope.pageClass = 'page-batches';
        $scope.batch = { NumberOfRuns:0 };
        $scope.Numbers = [{ id: '1' }];
        $scope.addNewNumber = addNewNumber;
        $scope.removeNumber = removeNumber;

        $scope.isReadOnly = false;
        $scope.AddBatch = AddBatch;

        function AddBatch() {
            $scope.batch.BatchNumbers = ''

            angular.forEach($scope.Numbers, function (Number) {
                $scope.batch.BatchNumbers += Number.number + ',';
            });

            $scope.batch.BatchNumbers = $scope.batch.BatchNumbers.slice(0, -1);

            AddBatchModel();
        }

        function AddBatchModel() {
            apiService.post('/api/batches/add', $scope.batch,
            addBatchSucceded,
            addBatchFailed);
        }


        function addBatchSucceded(response) {
            notificationService.displaySuccess($scope.batch.BatchNumbers + ' has been submitted to Media');
            $scope.batch = response.data;
        }

        function addBatchFailed(response) {
            console.log(response);
            notificationService.displayError(response.statusText);
        }

        function addNewNumber() {
            var newItemNo = $scope.Numbers.length + 1;
            $scope.Numbers.push({ 'id': newItemNo });
        }

        function removeNumber() {
            if ($scope.Numbers.length != 1) {
                var lastItem = $scope.Numbers.length - 1;
                $scope.Numbers.splice(lastItem);
            }
        }

    }

})(angular.module('media'));