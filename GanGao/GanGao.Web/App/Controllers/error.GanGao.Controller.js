(function () {
    var app = angular.module("GanGao.Services");

    app.controller("errorModal", ['$scope', '$uibModalInstance', 'error', function ($scope, $uibModalInstance, error) {
        var methods = {
            cancel: function () {
                $uibModalInstance.dismiss('cancel');
            },
            report: function () {
                $uibModalInstance.close(true);
            }
        };
        angular.extend($scope, methods, error);
    }]);
})()