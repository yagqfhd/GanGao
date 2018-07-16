/*
    Author: TS
    Date: 2016/12/24
    Description: It's the main entry point for require.
 */

var paths = {
    ///系统模块
    'jquery': 'Scripts/jquery-3.3.1',
    "text": "Scripts/text",
    "bootstrap":"Scripts/bootstrap",
    "director": "Scripts/director",
    'knockout': 'Scripts/knockout-3.4.2',
    "knockout-amd-helpers": "Scripts/knockout-amd-helpers",

    ///测试Require用户模块
    'RequireIndex-html': 'templates/Require/index.html',
    "RequireIndex-js": 'app/Require/index',

    "KnockoutIndex-html":"templates/knockout/index.html",
    'KnockoutIndex-js': 'app/knockout/index',

    /// director路由配置相关
    'director': 'Scripts/director',
    'Routes': 'app/director/routes',
    'AppRouter': 'app/director/router'
};

var baseUrl = '/';

require.config({
    baseUrl: baseUrl,
    paths: paths,
    shim: {
    }
});

require(["knockout", "KnockoutIndex-js", "knockout-amd-helpers", "text"], function (ko, KnockoutIndex) {
    ko.amdTemplateEngine.defaultPath = "/templates/knockout";
    ko.amdTemplateEngine.defaultSuffix = ".html";
    //ko.amdTemplateEngine.defaultRequireTextPluginName = "text";

    //ko.bindingHandlers.module.baseDir = "/templates/knockout";

    //fruits/vegetable modules have embedded template
    //ko.bindingHandlers.module.templateProperty = "embeddedTemplate";
    ko.applyBindings(KnockoutIndex);
});

//require(["jquery", "RequireIndex-js", "text!RequireIndex-html"],
//    function ($, module, html) {
//        console.log("Start test require html!");
//        $('#main').html(html);
//        console.log("Start test require js!");
//        module.TestRequireJs();
//    }
//);

require(['AppRouter'], function () {
    console.log('Start test router');
});


