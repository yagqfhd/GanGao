(function () {
    //创建一个 angularjs 服务模块
    var app = angular.module("GanGao.Services");

    // 为模块创建一个名为linkService服务，linkService返回一个列表对象
    app.factory("linkService", [function () {
        // 定义links为数组类型
        var links = [];
        links.push({
            name: '用户管理',
            urls: '/users/list'
        });
        links.push({
            name: '权限管理',
            urls: '/permissions/list'
        });
        links.push({
            name: '部门管理',
            urls: '/departments/list'
        });
        links.push({
            name: '角色管理',
            urls: '/roles/list'
        });        
        return links;
    }]);

})()