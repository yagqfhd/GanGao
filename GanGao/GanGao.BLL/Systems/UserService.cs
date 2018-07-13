using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GanGao.IBLL.Systems;
using GanGao.IDAL.ISystems;
using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System;
using GanGao.Common.DToMap;
using System.Globalization;

namespace GanGao.BLL
{
    /// <summary>
    /// 用户服务层
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        #region ///////////属性
        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AutoSaved { get; set; } = true;

        #endregion

        #region ////////////受保护的属性

        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected IUserRepository Repository { get; set; }

        /// <summary>
        /// 获取或设置 用户部门信息数据访问对象
        /// </summary>
        [Import]
        protected IUserDepartmentRepository UserDepartmentRepository { get; set; }

        /// <summary>
        /// 获取或设置 用户部门角色信息数据访问对象
        /// </summary>
        [Import]
        protected IUserDepartmentRoleRepository UserDepartmentRoleRepository { get; set; }
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
        IValidator<SysUser> Validator { get; set; }

        /// <summary>
        /// 获取或设置 DTO MODEL 映射服务
        /// </summary>
        [Import]
        IDtoMapService DtoMap { get; set; }

        /// <summary>
        /// 密码校验生成对象
        /// </summary>
        [Import]
        IPasswordValidator PasswordValidator { get; set; }
        #endregion

        #region 方法实现

        #region //// 标准曾删改服务接口实现
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> CreateAsync(DTOUser entity)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            if (string.IsNullOrWhiteSpace(entity.Password) == true)
                return new OperationResult(OperationResultType.ParamError, 
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PasswordNullError));
            // 实体模型转换
            var user = DtoMap.Map<SysUser>(entity);
            // 校验实体
            var validateResult =await Validator.ValidateAsync(user);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            user.PasswordHash = PasswordValidator.HashPassword(entity.Password);
            // 添加到实体集合中
            Repository.Insert(user, AutoSaved);
            // 返回正确
            return new OperationResult(OperationResultType.Success);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> DeleteAsync(string name)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(name, "name");
            // 获取实体
            var user = Repository.Entities.SingleOrDefault(d=>d.Name.Equals(name));
            if (user == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture, 
                    Systems.SysResources.UserNoExist
                    ,name)));
            try
            {
                // 从实体集合删除
                Repository.Delete(user.Id, AutoSaved);
            }   
            catch(DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }                         
            // 返回正确
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> UpdateAsync(DTOUser entity)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Name.Equals(entity.Name));
            if (user == null)
                return new OperationResult(OperationResultType.Warning, 
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , entity.Name));
            // 实体模型转换
            user = DtoMap.Map<DTOUser,SysUser>(entity,user);
            // 校验实体        
            var validateResult = await Validator.ValidateAsync(user);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                //更新实体
                Repository.Update(user, AutoSaved);
            }
            catch(DataAccessException ex)
            {
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
            // 返回正确
            return new OperationResult(OperationResultType.Success);
        }
        #endregion

        #region ////// 登录相关

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> ExistsByNameAsync(string userName)
        {
            PublicHelper.CheckArgument(userName, "userName");
            //获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Name == userName);
            if (user == null)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.QueryNull,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , userName)));
            }
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> ExistsByEmailAsync(string emailName)
        {
            PublicHelper.CheckArgument(emailName, "emailName");
            //获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Email == emailName);
            if (user == null)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.QueryNull,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.EmailNotExist
                    , emailName)));
            }
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 校验用户名称密码
        /// </summary>
        /// <param name="access"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> ValidatorUserAsync(string access,string password)
        {
            PublicHelper.CheckArgument(access, "access");
            PublicHelper.CheckArgument(password, "password");
            // 获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Name == access || m.Email == access);
            if (user == null)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.QueryNull,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , access)));
            }
            //校验密码
            if (PasswordValidator.VerifyHashedPassword(user.PasswordHash, password))
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Success));
            return Task.FromResult<OperationResult>(
                new OperationResult(OperationResultType.QueryNull,
                String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.PasswordError)));
        }
        #endregion

        #region /////// 查询
        /// <summary>
        /// 按照ID查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<DTOUser> FindByIdAsync(string id)
        {
            PublicHelper.CheckArgument(id, "id");
            var user = Repository.Entities.SingleOrDefault(m => m.Id.Equals(id));
            if (user == null)
            {
                return Task.FromResult<DTOUser>(null);
            }
            return Task.FromResult<DTOUser>(DtoMap.Map<DTOUser>(user));
        }
        /// <summary>
        /// 按照名称查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<DTOUser> FindByNameAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var user = Repository.Entities.SingleOrDefault(m => m.Name.Equals(name));
            if (user == null)
            {
                return Task.FromResult<DTOUser>(null);
            }
            return Task.FromResult<DTOUser>(DtoMap.Map<DTOUser>(user));
        }
        /// <summary>
        /// 按照Email查询用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task<DTOUser> FindByEmailAsync(string email)
        {
            PublicHelper.CheckArgument(email, "email");
            var user = Repository.Entities.SingleOrDefault(m => m.Email.Equals(email));
            if (user == null)
            {
                return Task.FromResult<DTOUser>(null);
            }
            return Task.FromResult<DTOUser>(DtoMap.Map<DTOUser>(user));
        }
        /// <summary>
        /// 按照用户名获取Email查询用户
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        public virtual Task<DTOUser> FindUserAsync(string access)
        {
            PublicHelper.CheckArgument(access, "access");
            var user = Repository.Entities.SingleOrDefault(m => m.Name == access || m.Email == access);
            if (user == null)
            {
                return Task.FromResult<DTOUser>(null);
            }
            var dtoUser = DtoMap.Map<DTOUser>(user);
            return Task.FromResult<DTOUser> (dtoUser);
        }
        /// <summary>
        /// 根据用户名或Email，密码获取用户
        /// </summary>
        /// <param name="access"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual Task<DTOUser> FindUserAsync(string access, string password)
        {
            PublicHelper.CheckArgument(access, "access");
            PublicHelper.CheckArgument(password, "password");
            // 获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Name == access || m.Email == access); 
            if (user == null)
            {
                return Task.FromResult<DTOUser>(null);
            }
            //校验密码
            if (PasswordValidator.VerifyHashedPassword(user.PasswordHash, password))
                return Task.FromResult<DTOUser>(DtoMap.Map<DTOUser>(user));
            return Task.FromResult<DTOUser>(null);
        }
        #endregion

        #region   /// 分页相关
        /// <summary>
        /// 获取指定页用户集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public virtual Task<DTOPage<DTOUser>> UserPageListAsync(int skip, int limit, string Order)
        {
            PublicHelper.CheckArgument(Order, "Order");
            PublicHelper.CheckArgument(skip, "Index",true);            
            PublicHelper.CheckArgument(limit, "Limit");

            //获取记录数
            var allCount = Repository.Entities.Count();
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
                var result = new DTOPage<DTOUser>();
                // 获取排序查询
                var query = Repository.Entities.OrderBy(Order);
                // 获取分页数据
                var users = query.Skip(skip).Take(limit).ToList();
                result.Total = allCount;
                result.Data = DtoMap.Map<IEnumerable<DTOUser>>(users);
                // 模型转换        
                return Task.FromResult<DTOPage<DTOUser>>(result);
            }
            catch(DataAccessException ex)
            {
                throw PublicHelper.ThrowBusinessException(ex.Message, ex);
            }
        }
        #endregion

        #region /////// 权限相关

        #endregion

        #region //// 部门相关增删改
        /// <summary>
        /// 添加用户到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public async Task<OperationResult> AddDepartmentAsync(string userName,string departmentName)
        {
            ///获取用户
            var user = Repository.Entities.FirstOrDefault(d => d.Name.Equals(userName));
            if (user == null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , userName)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if(department==null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , departmentName)));
            //检查用户是否包含部门
            var userDepartment = user.Departments.FirstOrDefault(d => d.DepartmentId.Equals(department.Id));
            if(userDepartment!=null)
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserInDepartment,
                    userName, departmentName)));
            ///添加部门
            try
            {
                user.Departments.Add(new UserDepartment { UserId = user.Id, DepartmentId = department.Id });
            }
            catch(DataAccessException ex)
            {
                return await Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }            
            return await Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 移除用户从部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> RemoveDepartmentAsync(string userName, string departmentName)
        {
            ///获取用户
            var user = Repository.Entities.FirstOrDefault(d => d.Name.Equals(userName));
            if (user == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , userName)));
            ///获取部门
            var department = departmentRepository.Entities.FirstOrDefault(d => d.Name.Equals(departmentName));
            if (department == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , departmentName)));
            //检查用户是否包含部门
            var userDepartment = user.Departments.FirstOrDefault(d => d.DepartmentId.Equals(department.Id));
            if (userDepartment == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNotInDepartment,
                    userName, departmentName)));
            ///移除部门
            try
            {
                UserDepartmentRepository.Delete(userDepartment, AutoSaved);
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
        /// 添加用户到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> AddRoleAsync(string userName, string departmentName, string roleName)
        {
            ///获取用户
            var user = Repository.Entities.FirstOrDefault(d => d.Name.Equals(userName));
            if (user == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist
                    , userName)));
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
            if(role==null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.RoleNotExist
                    , roleName)));
            // 校验用户部门
            var oldUserDepartment = UserDepartmentRepository.Entities.FirstOrDefault(
                d => d.UserId.Equals(user.Id) &&
                d.DepartmentId.Equals(department.Id));
            if(oldUserDepartment==null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNotInDepartment,
                    userName, departmentName)));
            ///校验用户部门角色
            var oldUserDepartmentRole = UserDepartmentRoleRepository.Entities.FirstOrDefault(
                d => d.UserId.Equals(user.Id) &&
                d.DepartmentId.Equals(department.Id) &&
                d.RoleId.Equals(role.Id));
            if(oldUserDepartmentRole!=null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserInRole,
                    userName, departmentName,roleName)));
            ///保存            
            try
            {
                UserDepartmentRoleRepository.Insert(new UserDepartmentRole
                {
                    UserId = user.Id,
                    DepartmentId = department.Id,
                    RoleId = role.Id
                }, AutoSaved);
            }
            catch(DataAccessException ex)
            {
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Error, ex.Message));
            }
            
            //成功
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }

        /// <summary>
        /// 移除用户从部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Task<OperationResult> RemoveRoleAsync(string userName, string departmentName, string roleName)
        {
            ///获取用户
            var user = Repository.Entities.FirstOrDefault(d => d.Name.Equals(userName));
            if (user == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNoExist,
                    userName)));
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
            // 校验用户部门
            var oldUserDepartment = UserDepartmentRepository.Entities.FirstOrDefault(
                d => d.UserId.Equals(user.Id) &&
                d.DepartmentId.Equals(department.Id));
            if (oldUserDepartment == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNotInDepartment,
                    userName, departmentName)));
            ///校验用户部门角色
            var oldUserDepartmentRole = UserDepartmentRoleRepository.Entities.FirstOrDefault(
                d => d.UserId.Equals(user.Id) &&
                d.DepartmentId.Equals(department.Id) &&
                d.RoleId.Equals(role.Id));
            if (oldUserDepartmentRole == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.ParamError,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.UserNotInRole,
                    userName, departmentName, roleName)));
            ///删除保存           
            try
            {
                UserDepartmentRoleRepository.Delete(oldUserDepartmentRole, AutoSaved);
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

        #endregion

        #region //////权限验证相关
        /// <summary>
        /// 获取用户具有的权限验证特征串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual  Task<IEnumerable<string>> GetDepartmentRoleListAsync(string name)
        {
            PublicHelper.CheckArgument(name, "userName");
            // 获取用户
            var user = Repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
            if (user == null) return Task.FromResult<IEnumerable<string>>(new List<string>());
            //根据用户ID获取验证特征串 （部门ID+角色ID）
            var results = UserDepartmentRoleRepository.Entities.Where(d => d.UserId.Equals(user.Id)).Select(v=>v.DepartmentId+v.RoleId);
            return Task.FromResult<IEnumerable<string>>(results.ToList());
        }
        #endregion
    }
}