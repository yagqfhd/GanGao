(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    var departments = {};
    var isLoad=false;
    app.factory("departmentService", ["http", "language", function (http, language) {
        var lang = language(true, "departmentForm");

        var methods = {
            alllist: function (refresh) {
                var ls = {};
                if (!isLoad || !!refresh) {
                    http.post(lang.API_PATH + "/department/page", { page: 1, limit: 1000 })
                    .then(function (data) {
                        departments =angular.copy(data);
                        isLoad = true;
                        angular.forEach(departments, function (dep) {
                            dep.IsSelected = false;
                        });
                        ls = angular.extend(ls, departments);
                    });
                }
                ls = angular.extend(ls, departments);
                return ls;
            },
            list: {
                "get": function (param) {
                    return http.post(lang.API_PATH + "/department/page", param);
                },
                "delete": function (id) {
                    return http.post(lang.API_PATH + "/department/delete/" + id);
                }
            },
            department: {
                "get": function (param) {
                    return http.get(lang.API_PATH + "/department/one", param);
                },
                "update": function (param) {
                    return http.post(lang.API_PATH + "/department/update", param);
                },
                "create": function (param) {
                    return http.post(lang.API_PATH + "/department/add", param);
                }
            }
        };
        return methods;
    }]);
})()