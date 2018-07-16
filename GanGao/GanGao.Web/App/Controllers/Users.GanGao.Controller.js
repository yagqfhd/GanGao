(function () {
    var app = angular.module("GanGao.Controllers");
    /// 用户列表显示控制器
    app.controller("users", ['$scope', 'usersService', 'utils', 'language', function ($scope, usersService, utils, language) {
        var lang = language(true, "userList");
        var service = usersService.list;
        var methods = {
            search: function (isPage) {
                $scope.loadingState = true;
                if (!isPage) $scope.current = 1;
                service.get({ page: $scope.current, limit: $scope.size }).then(function (data) {
                    $scope.list = data.Data;
                    $scope.total = data.Total;
                    $scope.loadingState = false;
                });
            },
            edit: function (item) {
                item.isModified = true;
                item.org = angular.copy(item);
            },
            remove: function (item) {
                var model = utils.confirm({ msg: lang.deleteUser, ok: lang.ok, cancel: lang.cancel });
                model.result.then(function () {
                    service.delete(item.Name).then(function (data) {
                        ///这里考虑对操作成功进行检查
                        utils.notify(lang.deleteSuccess, "success");
                        $scope.total = $scope.total - 1;
                        utils.remove($scope.list, item);                            
                    });
                });
            },
            size: 10
        };
        angular.extend($scope, methods);
        methods.search();

    }]);
    ///个体用户详细信息显示控制器
    app.controller("usersDetail",
        ['$scope', "language", "roleService",
            "usersService", "$routeParams", "utils", "$uibModal",
            function ($scope, language, roleService,
                usersService, $routeParams, utils, $uibModal) {
        var service = usersService.user;
        var lang = language(true, "userForm");        
        var org;     
        var methods = {
            lang: lang,
            cancel: function () {
                $uibModalInstance.dismiss('cancel');
            },
            isModified: !!$routeParams.id,            
            save: function () {
                var model = $scope.model;
                if (angular.equals(org, $scope.model)) {
                    return utils.confirm({ msg: lang.formNotModified, ok: lang.ok });
                }
                if (!$.trim(model.Name) || !$.trim(model.TrueName) || !$.trim(model.Email)) return;
                if ($scope.isModified) {
                    service.update(model).then(function (data) {
                       
                            utils.notify(lang.saveSuccess, "success");
                            org = angular.copy(model);
                            return;
                    });
                } else {
                    service.create(model).then(function (data) {
                            utils.notify(lang.saveSuccess, "success");
                            org = angular.copy(model);
                            return;
                    });
                }
            },            
            model: {},            
            remove: function (item) {
                var modal = utils.confirm({ msg: lang.confirmDelete, ok: lang.ok, cancel: lang.cancel });
                modal.result.then(function () {
                    service.deleteRole($scope.model.Id, item.Id).success(function (data) {
                            utils.notify(lang.deleteSuccess, "success");
                            utils.remove($scope.model.Roles, item);
                    });
                });
            },
            all: {},
        };
        roleService.list.all(false).then(function (data) {
            angular.forEach(data, function (l) {
                l.isChecked = false;
            });
            $scope.all = angular.copy(data);
        });
        if (methods.isModified) {
            service.get({ id: $routeParams.id, size: 100 }).then(function (data) {
                org = data;
                $scope.model = angular.copy(org);
                angular.forEach($scope.all, function (l) {
                    angular.forEach(org, function (k) {
                        if (k.Name == l.Name)
                            l.isChecked = true;
                    });
                });
            });
            methods.title = methods.lang.modifiedTitle;
        } else {
            methods.title = methods.lang.createTitle;
        }
        angular.extend($scope, methods);
    }]);

    ///显示用户部门信息控制器
    app.controller("usersDepartments",['$scope', "departmentService",function ($scope, departmentService) {
        var service = departmentService.list;
        var org = $scope.model.Departments;
        //angular.extend(org,$scope.model.Departments || []);
        var methods = {
            search: function () {
                var tmpDeps = {};
                service.all(true).then(function (data) {
                    angular.forEach(data, function (l) {
                        l.isChecked = true;
                        angular.forEach(org, function (v) {
                            if (l.Name == v.Name) {
                                l.isChecked = false;
                            }
                        });
                    });
                    angular.extend(tmpDeps,data);
                    //tmpDeps = data;
                });                
                $scope.departments = tmpDeps;
            }
        };                    
        methods.search();
        angular.extend($scope, methods);
      
    }]);


})()
