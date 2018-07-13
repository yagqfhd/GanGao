(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    app.factory("utils", ["$http", '$uibModal', function ($http, $uibModal) {
        var methods = {
            confirm: function (text) {
                return $uibModal.open({
                    templateUrl: '/app/views/utils/confirmModal.html',
                    backdrop: "static",
                    controller: "confirmmModal",
                    resolve: {
                        items: function () {
                            return text;
                        }
                    }
                });
            },
            notify: function (content, type) {
                $.notify(content, { type: type, delay: 1000, z_index: 1000000, placement: { from: 'top', align: 'right' } });
            },
            remove: function (list, item, fn) {
                angular.forEach(list, function (i, v) {
                    if (fn ? (fn(i, item)) : (i.$$hashKey === item.$$hashKey)) {
                        list.splice(v, 1);
                        return false;
                    }
                    return true;
                });
            },
            inArray: function (val, array, fn) {
                var has = false;
                angular.forEach(array, function (v) {
                    if (fn && fn(val, v) || val === v) {
                        has = true;
                        return false;
                    }
                    return true;
                });
                return has;
            }
        };
        return methods;
    }]);
})()