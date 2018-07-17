/// <reference path="E:\Web\GanGao\GanGao\GanGao.Web.Knockout\Scripts/knockout.mapping-latest.js" />

var paths = {
    ///系统模块
    'jquery': 'Scripts/jquery-3.3.1',
    "text": "Scripts/text",
    "bootstrap":"Scripts/bootstrap",
    "director": "Scripts/director",
    'knockout': 'Scripts/knockout-3.4.2',
    "knockout-amd-helpers": "Scripts/knockout-amd-helpers",
    "ko.mapping" : "Scripts/knockout.mapping-latest",

    /// 服务模块
    "http" : "App/services/http.GanGao.Ajax",
    "userService" : "app/services/User.GanGao.Services",

    /// 错误处理模块
    'Error-js': 'app/public/Error',
    'Error-html': 'templates/Error-html.html',

    /// director路由配置相关
    'director': 'Scripts/director',
    'Routes': 'app/director/routes',
    'AppRouter': 'app/director/router',

    /// Top导航模块定义
    'navbar-js': 'app/public/navbar',

    ///首页模板
    'Index-js': 'app/controllers/index-js',
    'index-html': 'templates/index-html.html',

    'Users-js': 'app/controllers/User.GanGao.Controllers',
    "Users-html" : "templates/users-html.html",

    ///模块管理模块定义
    'WebPageContrl': 'App/public/HtmlTemplateManager',

};

var baseUrl = '/';

require.config({
    baseUrl: baseUrl,
    paths: paths,
    shim: {
        /* TODO: provide all needed shims for non-AMD modules */
        //'Router': {
        //    exports: 'Router'
        //},        
    }
});

require(["knockout", "navbar-js", "text"], function (ko, navbar) {
    ko.components.register('topMenu', {
        viewModel: navbar,
        template: { require: 'text!/templates/navbar/navbar.html' }
    });
    console.log('绑定TOPMenu');
    ko.applyBindings(navbar, document.getElementsByTagName('navTop')[0]);
});



require(["knockout", "knockout-amd-helpers", "text"], function (ko) {
    ko.bindingHandlers.module.baseDir = "modules";
    ko.amdTemplateEngine.defaultPath = "templates";
    ko.amdTemplateEngine.defaultSuffix = ".html"
    //fruits/vegetable modules have embedded template
    ko.bindingHandlers.module.templateProperty = "embeddedTemplate";
});

require(['AppRouter'], function () {

    console.log('Start test router');
});


