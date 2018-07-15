(function () {
	//创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    app.factory("usersService", ["http", "language", function (http, language) {
        var lang = language(true, "userForm");
		var methods = {
			list: {
				"get": function (param) {
				    return http.post(lang.API_PATH + "/user/page", param);
				},
                "delete": function (id) {
                    return http.post(lang.API_PATH+"/user/delete/" + id);
                }
			},
			user: {
				"get": function (param) {
				    return http.get(lang.API_PATH+"/user/one", param);
				},
				"update": function (param) {
				    return http.post(lang.API_PATH+"/user/update", param);
				},
				"create": function (param) {
				    return http.post(lang.API_PATH + "/user/add", param);
				},
				"addDepartment":function(params){
				    return http.post(lang.API_PATH + "/user/department/add", param);
				},
				"deleteDepartment": function (params) {
				    return http.post(lang.API_PATH + "/user/department/remove", param);
				},
				"updateRoles": function (params) {
					return http.post("/api/user/UpdateRoles", params);
				},
				"deleteRole": function (id, roleId) {
					return http.post("/api/user/DeleteRole/" + id + "/" + roleId);
				}
			}
		};
		return methods;
	}]);
})()