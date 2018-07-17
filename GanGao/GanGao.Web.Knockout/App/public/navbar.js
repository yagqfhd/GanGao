define(['knockout'], function (ko) {
    
    var navbar = function TopMenuViewModel() {
        var self = this;        
        self.topMenus = [
            { MenuName: '用户管理', MenuUrl: '#/users', MenuTitle: '用户管理提示' },
            { MenuName: '部门管理', MenuUrl: '#/departments', MenuTitle: '部门管理提示' },
            { MenuName: '角色管理', MenuUrl: '#/roles', MenuTitle: '角色管理提示' },
        ];
        

        self.afterRender = function () {
            console.log('This is the function named afterRender in Navbar.js');
           // ko.applyBindings(self.data);
        }
    }
    return navbar;
    //var result = new TopMenuViewModel();
    //ko.applyBindings(new TopMenuViewModel());
    //return result
})

