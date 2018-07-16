(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    var departments = {};
    var isLoad=false;
    app.factory("departmentService", ["http", "language",'$q', function (http, language,$q) {
        var lang = language(true, "departmentForm");
        var methods = {            
            list: {
                "all": function (refresh) {
                    var ls = {};
                    var def = $q.defer();
                    if (!isLoad || !!refresh) {
                        http.post(lang.API_PATH + "/department/page", { page: 1, limit: 1000 })
                        .then(function (data) {
                            departments =angular.copy(data);
                            isLoad = true;
                            angular.forEach(departments, function (dep) {
                                dep.IsSelected = false;
                            });
                            //ls = angular.copy(departments);
                            angular.extend(ls, departments);
                            def.resolve(ls);
                        });
                    }
                    else {
                        angular.extend(ls, departments);
                        def.resolve(ls);
                    }
                    return def.promise;
                    //ls = angular.copy(departments);
                    //angular.extend(ls, departments);
                    //return ls;
                },
                "get": function (param) {
                    var ls = {};
                    http.post(lang.API_PATH + "/department/page", param)
                    .then(function (data) {
                        departments = angular.copy(data);
                        isLoad = true;
                        angular.forEach(departments, function (dep) {
                            dep.IsSelected = false;
                        });
                        ls = angular.copy(departments);
                    });
                    ls = angular.copy(departments);
                    return ls;
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