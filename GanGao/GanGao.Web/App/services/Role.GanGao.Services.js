(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    var roles = {};
    var isLoad=false;
    app.factory("roleService", ["http", "language", "$q", function (http, language, $q) {
        var lang = language(true, "roleForm");
        var methods = {
            list: {
                "get": function (param) {
                    var ls = {};
                    http.post(lang.API_PATH + "/role/page", param)
                    .then(function (data) {
                        roles = angular.copy(data);
                        isLoad = true;
                        angular.forEach(roles, function (dep) {
                            dep.IsSelected = false;
                        });
                        ls = angular.copy(roles);
                    })
                    .catch(function (error) {//加上catch 
                        console.log(error);
                    });
                    return ls;
                },
                "all": function (refresh) {
                    var ls = {};
                    var def = $q.defer();
                    if (!isLoad || !!refresh) {
                        http.post(lang.API_PATH + "/role/page", { page: 1, limit: 1000 })
                        .then(function (data) {
                            //roles = angular.copy(data);
                            angular.extend(roles, data);
                            isLoad = true;
                            angular.forEach(roles, function (dep) {
                                dep.IsSelected = false;
                            });                            
                            //ls = angular.copy(roles);
                            angular.extend(ls, roles);
                            def.resolve(ls);
                        })
                        .catch(function (error) {//加上catch 
                            console.log(error);
                            def.reject(err);
                        });
                    }
                    else {
                        angular.extend(ls, roles);
                        def.resolve(ls);
                    }
                    return def.promise;
                },
                "delete": function (id) {
                    return http.post(lang.API_PATH + "role/delete/" + id);
                }
            },
        };

        return methods;
    }]);

})()
    
