using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GanGao.IBLL.Systems;
using GanGao.IDAL.ISystems;
using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using GanGao.MEF;
using GanGao.Common.DToMap;

namespace GanGao.BLL
{
    /// <summary>
    /// 用户服务层
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        public UserService() : base()
        {
            AutoSaved = false;
            /// MEF IoC映射
            //RegisgterMEF.regisgter().ComposeParts(this);

        }

        #region 属性
        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AutoSaved { get; set; }

        #region 受保护的属性

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
            // 实体模型转换
            var user = DtoMap.Map<SysUser>(entity);
            // 校验实体
            var validateResult =await Validator.ValidateAsync(user);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
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
            if(user == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Warning,"用户不存在！")
                );
            // 从实体集合删除
            Repository.Delete(user.Id, AutoSaved);
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
                return new OperationResult(OperationResultType.Warning, "用户不存在！");
            // 实体模型转换
            user = DtoMap.Map<DTOUser,SysUser>(entity,user);            
            // 校验实体
            var validateResult = await Validator.ValidateAsync(user);
            if (validateResult.ResultType == OperationResultType.Success)
                return new OperationResult(OperationResultType.Failed, "用户不存在");
            //更新实体
            Repository.Update(user, AutoSaved);
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
                return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。"));
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
            PublicHelper.CheckArgument(emailName, "userName");
            //获取用户
            var user = Repository.Entities.SingleOrDefault(m => m.Email == emailName);
            if (user == null)
            {
                return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。"));
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
                return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。"));
            }
            //校验密码
            if (PasswordValidator.VerifyHashedPassword(user.PasswordHash, password))
                return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull, "指定账号的用户密码不正确。"));
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
        /// 按照名次查询用户
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
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTOUser>> UserPageListAsync(int Index, int Limit, string Order)
        {
            PublicHelper.CheckArgument(Order, "Order");
            PublicHelper.CheckArgument(Index, "Index");
            PublicHelper.CheckArgument(Limit, "Limit");
            //获取记录数
            var allCount = Repository.Entities.Count();
            // 计算跳过记录数
            var skip = (Index - 1) * Limit;
            if (skip < 0 || skip > allCount || Limit<0)
                return Task.FromResult <IEnumerable<DTOUser>>(null);
            // 获取排序查询
            var query = Repository.Entities.OrderBy(Order);
            // 获取分页数据
            var users = query.Skip(skip).Take(Limit).ToList();
            // 模型转换        
            return Task.FromResult<IEnumerable<DTOUser>>(DtoMap.Map<IEnumerable<DTOUser>>(users));
        }
        #endregion

        #region /////// 权限相关

        #endregion
        #endregion
    }
}