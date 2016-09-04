(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope','apiService', 'notificationService'];

    function indexCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
        $scope.loadingRuns = true;
        $scope.loadingBatches = true;
        $scope.isReadOnly = true;

        $scope.Runs = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('/api/runs', null,
                        runsLoadCompleted,
                        runsLoadFailed);

            apiService.get("/api/batches", null,
                batchesLoadCompleted,
                batchesLoadFailed);
        }

        function runsLoadCompleted(result) {
            $scope.Runs = result.data;
            $scope.loadingRuns = false;
        }

        function batchesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function runsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function batchesLoadCompleted(result) {
            var batches = result.data;

            Morris.Bar({
                element: "batches-bar",
                data: batches,
                xkey: ["BatchNumbers"],
                ykeys: ["NumberOfRuns"],
                labels: ["Number Of Runs"],
                barRatio: 0.5,
                xLabelAngle: 55,
                hideHover: "auto",
                resize: 'true'
            });

            $scope.loadingBatches = false;
        }

        loadData();
    }

})(angular.module('media'));