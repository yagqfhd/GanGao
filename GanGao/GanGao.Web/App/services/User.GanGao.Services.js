(function () {
	//创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

	app.factory("usersService", ["http", function (http) {
		var methods = {
			list: {
				"get": function (param) {
					return http.post("http://localhost:8082/api/user/page", param);
				},
                "delete": function (id) {
                    return http.post("http://localhost:8082/api/user/delete/" + id);
                }
			},
			user: {
				"get": function (param) {
					return http.get("/api/user/UserInfo", param);
				},
				"update": function (param) {
					return http.post("/api/user/UpdateUser", param);
				},
				"create": function (param) {
					return http.post("/api/user/AddUser", param);
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