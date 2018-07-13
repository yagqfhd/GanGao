'use strict';

(function () {
    var service = angular.module('GanGao.Services', []);

    service.factory("http", ['$http', '$modal', 'utils', 'language', function ($http, $modal, utils, language) {
        var lang = language(true);

        var methods = {
            'call': function (type, url, params, data) {
                return $http({ method: type, url: url, params: params, data: data }).success(methods.success).error(methods.errorModal);
            },
            'success': function (data) {
                if (data.Message)
                    utils.confirm({ msg: lang[data.Message], ok: lang.ok });
                return data;
            },
            'errorModal': function (data) {
                $modal.open({
                    templateUrl: 'utils-errorModal ',
                    backdrop: "static",
                    controller: "errorModal",
                    resolve: {
                        error: function () {
                            return data;
                        }
                    }
                });
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
