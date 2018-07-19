define(['knockout', "userService", "mapping", "ganGaoModel", "bootstrapTable"], function (ko, userService, mapping, ganGaoModel) {
    //出理 knockout mapping require导入BUG
    ko.mapping = mapping;

    function userGanGaoViewModel() {
        var self = this;
        ///统一路由模块使用
        this.init = function () { };
        this.controllers = {
            "/":function(){}
        };
        this.afterRender = function (element) {
            this.getUsers();
            console.log("Successful to load 用户管理 page");
        };
        ///ViewModel 使用的变量
        this.Users=ko.observableArray();
        this.page = ko.observable(1);
        this.limit = ko.observable(10);
        this.loadingState = ko.observable(true);
        /// 获取用户列表的函数
        this.getUsers = function () {
            var that = this;
            userService.list.get({ data: { page: this.page(), limit: this.limit() } }).done(function (data) {
                that.Users = ko.mapping.fromJS(data.Data, {}, that.Users);
                that.loadingState(false);
            }).fail(function (err) {
                ///处理错误
            });
        };
        /// 添加用户处理
        this.addClick = function () {
            ganGaoModel.show("Index");
            console.log("用户管理 page AddClick事件");
        };
        this.remove = function () {
            var item = this;
            var that = self;
            userService.list.delete(item.Name()).done(function (data) {
                self.Users.remove(item);
                console.log("用户管理 page 删除用户成功事件" + item.Name());
            }).fail(function (err) {

            });
            
            console.log("用户管理 page AddClick事件"+item.Name());
        };
    };

    ///定义用户显示模型
    var viewModel = {
        ///统一格式路由使用
        init: function () {            
            
        },
        controllers : {
            '/': function () { }
        },
        afterRender: function (element) {            
            viewModel.getUsers();
            console.log("Successful to load 用户管理 page");
        },
        
        Users: ko.observableArray(),
        page:ko.observable(1),
        limit:ko.observable(10),
        getUsers: function () {
            userService.list.get({ data: { page: viewModel.page(), limit: viewModel.limit } }).done(function (data) {
                viewModel.Users = ko.mapping.fromJS(data.Data, {}, viewModel.Users);                
            }).fail(function (err) {
                ///处理错误
            });
        }
    }

    //return viewModel;
    return new userGanGaoViewModel();
});