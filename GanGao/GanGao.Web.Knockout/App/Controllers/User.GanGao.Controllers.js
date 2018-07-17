define(['knockout', "userService","ko.mapping"], function (ko, userService) {
    function UserViewModel() {
        var self = this;
        ///统一格式路由使用
        self.init = function () { };
        self.controllers = {
            '/': function () { }
        };
        self.afterRender = function (element) {
            console.log("Successful to load CustomerIntroductionViewModel page");
        };
        
        /// 自定义监控属性
        /// 分页使用
        self.page = ko.observable(1);
        self.limit = ko.observable(10);

        /// 用户列表分页数据
        self.userList = function () {
            var result = ko.observableArray();
            userService.list.get({ page: self.page, limit: self.limit }).done(function (data) {
                result = ko.mapping.fromJS(data);
            }).fail(function (err) {
                ///处理错误
            });
            return result;
        };

        
    }
    return new UserViewModel();
});