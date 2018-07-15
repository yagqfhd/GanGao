'use strict';
// 立即执行函数
(function () {
    
    var app = angular.module("GanGao", ['ngRoute', "ui.bootstrap","GanGao.Services", "GanGao.Controllers"]);

    app.config(['$routeProvider', function ($routeProvider) {
        // 路由配置
        var route = $routeProvider;
        route.when("/users/list", { controller: 'users', templateUrl: '/App/views/users/list.html' });
        route.when("/users/user/", { controller: 'usersDetail', templateUrl: '/App/views/users/detail.html' });
        route.when("/users/user/:id", { controller: 'usersDetail', templateUrl: '/App/views/users/detail.html' });
        route.when("/roles/list", { controller: 'roles', templateUrl: '/roles-list' });
        route.when("/permissions/list", { controller: 'permissions', templateUrl: '/permissions-list' });
        route.when("/", { redirectTo: '/' });
        route.otherwise({ templateUrl: '/App/views/utils/404.html' });
    }
    ]);

    ///添加如下配置
    ///这个是angular1.6默认给hash路由上添加了!(感叹号)，导致出错，
    ///修改方法如下(添加配置，去掉默认前缀感叹号)：
    app.config(['$locationProvider', function($locationProvider) {
        $locationProvider.hashPrefix("");
    }]);
})();