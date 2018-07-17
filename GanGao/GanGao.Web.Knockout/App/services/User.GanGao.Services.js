define(["http"], function (http) {
    var apiBase = "http://127.0.0.1/API/Users/";
    var userService= {
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
    return userService;

});