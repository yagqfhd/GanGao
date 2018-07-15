(function () {
    // 创建GanGao.Controllers模块
    var app = angular.module("GanGao.Controllers", []);

    // 定义navigation控制器，该控制器被index.html所使用
    app.controller("navigation", ['$scope', '$location', '$routeParams', "linkService", function ($scope, $location, $routeParams, linkService) {
        // $scope相当于DataContext，设置ls的属性为linkService,linkService返回的是一个数组
        $scope.ls = linkService;
        //绑定路由跳转成功，左导航获取和主导航的选中状态
        $scope.$on('$locationChangeSuccess', function (route, url) {
            //console.dir(url);
            //var v = /#\/([^\/]+)/.exec(url);
            var v = /#\/([^\/]+)/.exec(url);
            if (!v) return;
            
            //获取所有与当前主URL匹配的子连接
            angular.forEach(linkService, function (l) {
                //l.isActive = false;
                //l.isActive = tmpUrl.indexOf(l.urls) !== -1;
                if (l.urls.indexOf(v[1]) !== -1) {
                    $scope.urls = l.urls;
                    l.isActive = true;
                }

                //angular.forEach(l.urls, function (u) {
                //    console.log("l.urls.link=", u.link);
                //    u.isActive = url.indexOf(u.link) !== -1;
                //    if (u.link.indexOf(v[1]) !== -1) {
                //        $scope.urls = l.urls;
                //        l.isActive = true;
                //    }
                //});
            });
        });
    }]);
})()