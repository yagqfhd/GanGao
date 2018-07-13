(function () {
    // 创建GanGao.Controllers模块
    var app = angular.module("GanGao.Controllers", []);

    // 定义navigation控制器，该控制器被index.html所使用
    app.controller("navigation", ['$scope', '$location', '$routeParams', "linkService", function ($scope, $location, $routeParams, linkService) {
        // $scope相当于DataContext，设置ls的属性为linkService,linkService返回的是一个数组
        $scope.ls = linkService;
    }]);
})()