(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    var roles = {};
    var isLoad=false;
    app.factory("roleService", ["http", "language", function (http, language) {
        var lang = language(true, "roleForm");
        var methods = {
            list: {
                "get": function (param,refresh) {
                    var ls = {};
                    var start = (param.page - 1) * param.limit;
                    var end = start + param.limit;
                    if (!isLoad || !!refresh) {
                        http.post(lang.API_PATH + "/role/page", { page: 1, limit: 1000 })
                        .then(function (data) {
                            roles = angular.copy(data);
                            isLoad = true;
                            angular.forEach(roles, function (dep) {
                                dep.IsSelected = false;
                            });
                            ls = roles.slice(start, end); //angular.extend(ls, roles);
                        });
                    }
                    ls = roles.slice(start, end);
                    return ls;
                },
                "delete": function (id) {
                    return http.post(lang.API_PATH + "role/delete/" + id);
                }
            },
        };

        return methods;
    }]);

})()
    
