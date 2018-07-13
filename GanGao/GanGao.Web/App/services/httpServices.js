'use strict';

(function () {
    var app = angular.module('GanGao.Services',[]);

    app.factory("http", ['$http', '$uibModal', function ($http, $uibModal) {
        var methods = {
            'call': function (type, url, params, data) {
                return $http({ method: type, url: url, params: params, data: data, headers: "Content-Type:application/x-www-form-urlencoded" })
                    .then(methods.success)
                    .catch(methods.errorModal);
            },
            'success': function (data) {
                return data.data;
            },
            'errorModal': function (data) {
                $uibModal.open({
                    templateUrl: '/app/views/utils/errorModal.html',
                    backdrop: "static",
                    controller: "errorModal",
                    resolve: {
                        error: function () {
                            return data;
                        }
                    }
                });
                //console.dir(data);
            },
            'get': function (url, params) {
                return methods.call('GET', url, params);
            },
            'post': function (url, data) {
                return methods.call('POST', url, null, data);
            }
        };
        return methods;
    }]);
})()
