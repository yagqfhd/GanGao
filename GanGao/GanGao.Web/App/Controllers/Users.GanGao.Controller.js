(function () {
    var app = angular.module("GanGao.Controllers");

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

    app.controller("usersDetail", ['$scope', "language", "usersService", "$routeParams", "utils", "$uibModal", function ($scope, language, usersService, $routeParams, utils, $uibModal) {
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
                if (!$.trim(model.Name) || !$.trim(model.RealName) || !$.trim(model.Email)) return;
                if (model.Id > 0) {
                    service.update(model).success(function (data) {
                        if (data.IsSaved) {
                            utils.notify(lang.saveSuccess, "success");
                            org = angular.copy(model);
                            return;
                        }
                    });
                } else {
                    service.create(model).success(function (data) {
                        if (data.IsCreated) {
                            utils.notify(lang.saveSuccess, "success");
                            $scope.model.Id = data.Id;
                            org = angular.copy(model);
                            return;
                        }
                    });
                }
            },
            append: function () {
                var modal = $modal.open({
                    templateUrl: 'users-chooseRoles ',
                    backdrop: "static",
                    controller: "chooseRoles",
                    size: "lg",
                    resolve: {
                        params: function () {
                            return $scope.model;
                        }
                    }
                });

                modal.result.then(function () {
                    usersService.user.updateRoles($scope.model).success(function (data) {
                        if (data.IsSaved) {
                            methods.getRoles();
                        }
                    });
                });
            },
            model: {},
            remove: function (item) {
                var modal = utils.confirm({ msg: lang.confirmDelete, ok: lang.ok, cancel: lang.cancel });
                modal.result.then(function () {
                    service.deleteRole($scope.model.Id, item.Id).success(function (data) {
                        if (data.IsDeleted) {
                            utils.notify(lang.deleteSuccess, "success");
                            utils.remove($scope.model.Roles, item);
                        }
                    });
                });
            },
        };
        if (methods.isModified) {
            service.get({ id: $routeParams.id, size: 100 }).success(function (data) {
                org = data.Data;
                $scope.model = angular.copy(org);
            });
            methods.title = methods.lang.modifiedTitle;
        } else {
            methods.title = methods.lang.createTitle;
        }
        angular.extend($scope, methods);
    }]);
})()
