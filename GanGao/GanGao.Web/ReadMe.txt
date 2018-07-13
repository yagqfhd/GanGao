AngularJS 实现简单的后台管理 http://www.cnblogs.com/zhili/p/AngularJSPrivilege.html
项目结构
App/images // 存放Web前端使用的图片资源

App/Styles // 存放样式文件

App/scripts // 整个Web前端用到的脚本文件
                / Controllers // angularJS控制器模块存放目录
               /  directives // angularJs指令模块存放目录
              /   filters  // 过滤器模块存放目录
              /   services // 服务模块存放目录
            / app.js // Web前端程序配置模块(路由配置)
App/Modules  // 项目依赖库，angular、Bootstrap、Jquery库

App/Views // AngularJs视图模板存放目录

引用
//类库依赖文件
            bundles.Add(new ScriptBundle("~/js/base/lib").Include(
                    "~/app/modules/jquery-1.11.2.min.js",
                    "~/app/modules/angular/angular.min.js",
                    "~/app/modules/angular/angular-route.min.js",
                    "~/app/modules/bootstrap/js/ui-bootstrap-tpls-0.13.0.min.js",
                    "~/app/modules/bootstrap-notify/bootstrap-notify.min.js"
                   ));
            //angularjs 项目文件
            bundles.Add(new ScriptBundle("~/js/angularjs/app").Include(
                    "~/app/scripts/services/*.js",
                    "~/app/scripts/controllers/*.js",
                    "~/app/scripts/directives/*.js",
                    "~/app/scripts/filters/*.js",
                    "~/app/scripts/app.js"));
            //样式
            bundles.Add(new StyleBundle("~/js/base/style").Include(
                    "~/app/modules/bootstrap/css/bootstrap.min.css",
                    "~/app/styles/dashboard.css",
                    "~/app/styles/console.css"
                    ));


Privilege Management Web API
PrivilegeManagement 是一个简易的权限管理系统。该项目后台是基于Asp.net
Web API前端采用AngularJs+Boostrap来实现的。 
https://github.com/lizhi5753186/PrivilegeManagement

Online Store采用了面向领域驱动的经典分层架构，并且为了展示微软.NET技术在企业级应用开发
中的应用， 它所使用的第三方组件也几乎都是微软提供的：Entity Framework、ASP.NET MVC、
Unity IoC、Unity AOP、Enterprise Library Caching等（用于记录日志的log4net除外，
但log4net本身也是众所周知的框架），所以，开发人员只需要打开Online Store的源程序，
就能够很清楚地看到系统的各个组件是如何组织在一起并协同工作的。 
https://github.com/lizhi5753186/OnlineStore

支持CQRS的DDD框架

https://github.com/lizhi5753186/Fastworks