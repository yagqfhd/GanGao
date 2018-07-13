(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    app.controller("confirmmModal", ['$scope', '$uibModalInstance', 'items', function ($scope, $uibModalInstance, items) {
        var methods = {
            ok: function () {
                $uibModalInstance.close(true);
            },
            cancel: function () {
                $uibModalInstance.dismiss('cancel');
            },
            text: items
        };
        $.extend($scope, methods);
    }]);
})()