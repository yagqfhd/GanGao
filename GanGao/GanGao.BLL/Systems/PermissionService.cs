using GanGao.Common;
using GanGao.Common.DToMap;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using GanGao.IBLL.Systems;
using GanGao.IDAL.ISystems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GanGao.BLL.Systems
{
    /// <summary>
    /// 权限服务层
    /// </summary>
    [Export(typeof(IPermissionService))]
    public class PermissionService : CoreServiceBase, IPermissionService
    {
        #region ///////////属性
        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AutoSaved { get; set; } = true;

        #endregion

        #region ////////////受保护的属性

        /// <summary>
        /// 获取或设置 数据访问对象
        /// </summary>
        [Import]
        protected IPermissionRepository repository { get; set; }
        /// <summary>
        /// 获取或设置 部门信息数据访问对象
        /// </summary>
        [Import]
        protected IPermissionDepartmentRepository permissionDepartmentRepository { get; set; }

        /// <summary>
        /// 获取或设置 部门角色信息数据访问对象
        /// </summary>
        [Import]
        protected IPermissionDepartmentRoleRepository permissionDepartmentRoleRepository { get; set; }
        /// <summary>
        /// 部门信息存储访问对象
        /// </summary>
        [Import]
        protected IDepartmentRepository departmentRepository { get; set; }
        /// <summary>
        /// 角色信息存储访问对象
        /// </summary>
        [Import]
        protected IRoleRepository roleRepository { get; set; }
        /// <summary>
        /// 获取或设置 用户信息校验对象
        /// </summary>
        [Import]
        IValidator<SysPermission> Validator { get; set; }

        /// <summary>
        /// 获取或设置 DTO MODEL 映射服务
        /// </summary>
        [Import]
        IDtoMapService DtoMap { get; set; }
        #endregion

        #region //// 标准曾删改服务接口实现
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> CreateAsync(DTOPermission entity)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 实体模型转换
            var permission = DtoMap.Map<SysPermission>(entity);
            // 校验实体
            var validateResult = await Validator.ValidateAsync(permission);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                // 添加到实体集合中
                repository.Insert(permission, AutoSaved);
                // 返回正确
                return new OperationResult(OperationResultType.Success);
            }
            catch (DataAccessException ex)
            {
                return new OperationResult(OperationResultType.Error,
                    ex.Message);
            }

        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> DeleteAsync(string name)
        {
            /// 删除角色，考虑在哪一层删除角色相关的用户，权限部门角色
            /// 这个问题在以后考虑，到时修改本方法
            //校验参数！=NULL
            PublicHelper.CheckArgument(name, "name");
            // 获取实体
            var permission = repository.Entities.SingleOrDefault(d => d.Name.Equals(name));
            if (permission == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist
                    , name)));
            try
            {
                // 从实体集合删除
                repository.Delete(permission.Id, AutoSaved);
                // 返回正确
                return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
            }
            catch (DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> UpdateAsync(DTOPermission entity)
        {
            /// 更新角色信息
            /// 考虑是否需要限制更改角色名称
            /// 其他更新信息同样存在这个问题
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 获取用户
            var permission = repository.Entities.SingleOrDefault(m => m.Name.Equals(entity.Name));
            if (permission == null)
                return new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist
                    , entity.Name));
            // 实体模型转换
            permission = DtoMap.Map<DTOPermission, SysPermission>(entity, permission);
            // 校验实体        
            var validateResult = await Validator.ValidateAsync(permission);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                //更新实体
                repository.Update(permission, AutoSaved);
                // 返回正确
                return new OperationResult(OperationResultType.Success);
            }
            catch (DataAccessException ex)
            {
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
        }
        #endregion

        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTOPermission>> PageListAsync(int skip, int limit, string order)
        {
            PublicHelper.CheckArgument(order, "Order");
            PublicHelper.CheckArgument(skip, "Skip", true);
            PublicHelper.CheckArgument(limit, "Limit");
            //获取记录数
            var allCount = repository.Entities.Count();
            // 计算跳过记录数
            if (skip < 0 || skip > allCount || limit < 1)
            {
                throw PublicHelper.ThrowBusinessException(
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PageError,
                    "跳过记录数大于总数或每页数小于1"));
            }
            try
            {
                // 获取排序查询
                var query = repository.Entities.OrderBy(order);
                // 获取分页数据
                var permissions = query.Skip(skip).Take(limit).ToList();
                // 模型转换        
                return Task.FromResult<IEnumerable<DTOPermission>>(DtoMap.Map<IEnumerable<DTOPermission>>(permissions));
            }
            catch (DataAccessException ex)
            {
                throw PublicHelper.ThrowBusinessException(ex.Message, ex);
            }
        }

        #region ///// 查询

        /// <summary>
        /// 按照名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<DTOPermission> FindByNameAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var role = repository.Entities.SingleOrDefault(m => m.Name.Equals(name));
            if (role == null)
            {
                return Task.FromResult<DTOPermission>(null);
            }
            return Task.FromResult<DTOPermission>(DtoMap.Map<DTOPermission>(role));
        }
        
        /// <summary>
        /// 按照控制名查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTOPermission>> FindByControllerAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var roles = repository.Entities.Where(m => m.ControllerName.Equals(name)).ToList();
            return Task.FromResult<IEnumerable<DTOPermission>>(DtoMap.Map<IEnumerable<DTOPermission>>(roles));
        }

        /// <summary>
        /// 按照动作名查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTOPermission>> FindByActionAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var roles = repository.Entities.Where(m => m.ActionName.Equals(name)).ToList();            
            return Task.FromResult< IEnumerable<DTOPermission>>(DtoMap.Map< IEnumerable<DTOPermission>>(roles));
        }
        #endregion

        #region //// 部门相关增删改
        /// <summary>
        /// 添加到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public async Task<OperationResult> AddDepartmentAsync(string name, string departmentName)
        {
            ///获取用户
            var permission = repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (permission == null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist
                    , name)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if (department == null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , departmentName)));
            //检查用户是否包含部门
            var userDepartment = permission.Departments.FirstOrDefault(d => d.DepartmentId.Equals(department.Id));
            if (userDepartment != null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionInDepartment,
                    name, departmentName)));
            ///添加部门
            try
            {
                permissionDepartmentRepository.Insert(new PermissionDepartment
                {
                    PermissionId = permission.Id,
                    DepartmentId = department.Id
                },AutoSaved);
            }
            catch (DataAccessException ex)
            {
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
            return await Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 移除从部门中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> RemoveDepartmentAsync(string name, string departmentName)
        {
            ///获取
            var permission = repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (permission == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist
                    , name)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if (department == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , departmentName)));
            //检查是否包含部门
            var permissionDepartment = permission.Departments.FirstOrDefault(d => d.DepartmentId.Equals(department.Id));
            if (permissionDepartment == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionInDepartment,
                    name, departmentName)));
            ///移除部门
            try
            {
                permissionDepartmentRepository.Delete(permissionDepartment, AutoSaved);
                //UserDepartmentRepository.Delete(userDepartment, AutoSaved);
            }
            catch (DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }
        #endregion

        #region //// 角色相关
        /// <summary>
        /// 添加到角色中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> AddRoleAsync(string name, string departmentName, string roleName)
        {
            ///获取
            var permission = repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (permission == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist
                    , name)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if (department == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , departmentName)));
            //获取角色
            var role = roleRepository.Entities.FirstOrDefault(d => d.Name.Equals(roleName));
            if (role == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.RoleNotExist
                    , roleName)));
            // 校验是否在部门中
            var permissionDepartment = permissionDepartmentRepository.Entities.FirstOrDefault(
                d => d.PermissionId.Equals(permission.Id) &&
                d.DepartmentId.Equals(department.Id));
            if (permissionDepartment == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotInDepartment,
                    name, departmentName)));
            ///校验是否部门角色中
            var permissionDepartmentRole = permissionDepartmentRoleRepository.Entities.FirstOrDefault(
                d => d.PermissionId.Equals(permission.Id) &&
                d.DepartmentId.Equals(department.Id) &&
                d.RoleId.Equals(role.Id));
            if (permissionDepartmentRole != null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionInRole,
                    name, departmentName, roleName)));
            ///保存            
            try
            {
                permissionDepartmentRoleRepository.Insert(new PermissionDepartmentRole
                {
                    PermissionId = permission.Id,
                    DepartmentId = department.Id,
                    RoleId = role.Id
                }, AutoSaved);
            }
            catch (DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }

            //成功
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 移除从部门角色中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> RemoveRoleAsync(string name, string departmentName, string roleName)
        {
            ///获取
            var permission = repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (permission == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotExist,
                    name)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if (department == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist,
                    departmentName)));
            //获取角色
            var role = roleRepository.Entities.FirstOrDefault(d => d.Name.Equals(roleName));
            if (role == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.RoleNotExist,
                    roleName)));
            // 校验部门
            var permissionDepartment = permissionDepartmentRepository.Entities.FirstOrDefault(
                d => d.PermissionId.Equals(permission.Id) &&
                d.DepartmentId.Equals(department.Id));
            if (permissionDepartment == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotInDepartment,
                    name, departmentName)));
            ///校验部门角色
            var permissionDepartmentRole = permissionDepartmentRoleRepository.Entities.FirstOrDefault(
                d => d.PermissionId.Equals(permission.Id) &&
                d.DepartmentId.Equals(department.Id) &&
                d.RoleId.Equals(role.Id));
            if (permissionDepartmentRole == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PermissionNotInRole,
                    name, departmentName, roleName)));
            ///删除保存           
            try
            {
                permissionDepartmentRoleRepository.Delete(permissionDepartmentRole, AutoSaved);
            }
            catch (DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
            //成功
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }
        #endregion

        #region //////权限验证相关
        /// <summary>
        /// 获取用户具有的权限验证特征串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<string>> GetDepartmentRoleListAsync(string name)
        {
            PublicHelper.CheckArgument(name, "userName");
            // 获取权限
            var permission = repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (permission == null) return Task.FromResult<IEnumerable<string>>(new List<string>());
            //根据权限ID获取验证特征串 （部门ID+角色ID）
            var results = permissionDepartmentRoleRepository.Entities.Where(d => d.PermissionId.Equals(permission.Id)).Select(v => v.DepartmentId + v.RoleId);
            return Task.FromResult<IEnumerable<string>>(results.ToList());
        }
        #endregion
    }
}