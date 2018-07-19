define(["http"], function (http) {
    var apiBase = "http://localhost:8082/api/user";
    var userService= {
        list: {
            "get": function (param) {
                return http.post(apiBase + "/page", param);
            },
            "delete": function (id) {
                return http.post(apiBase + "/delete/" + id);
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
                return http.post(apiBase + "/user/add", param);
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
    return userService;

});