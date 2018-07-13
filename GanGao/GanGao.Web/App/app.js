'use strict';
// 立即执行函数
(function () {
    var app = angular.module("GanGao", ['ngRoute', "GanGao.Controllers","GanGao.Services"]);
    app.config(['$routeProvider', function ($routeProvider) {
        // 路由配置
        var route = $routeProvider;
        route.when("/users/list", { controller: 'users', templateUrl: '/App/views/users/list.html' });
        //route.when("/users/user/", { controller: 'usersDetail', templateUrl: '/users-detail' });
        //route.when("/users/user/:id", { controller: 'usersDetail', templateUrl: '/users-detail' });
        //route.when("/roles/list", { controller: 'roles', templateUrl: '/roles-list' });
        //route.when("/permissions/list", { controller: 'permissions', templateUrl: '/permissions-list' });
        //route.when("/", { redirectTo: '/' });
        route.otherwise({ templateUrl: '/App/views/utils/404.html' });
    }
    ]);
})();