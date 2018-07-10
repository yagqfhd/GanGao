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
    /// 角色服务层
    /// </summary>
    [Export(typeof(IRoleService))]
    public class RoleService : CoreServiceBase, IRoleService
    {
        #region ///////////属性
        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AutoSaved { get; set; } = true;

        #endregion

        #region ////////////受保护的属性

        /// <summary>
        /// 获取或设置 角色信息数据访问对象
        /// </summary>
        [Import]
        protected IRoleRepository Repository { get; set; }
        /// <summary>
        /// 获取或设置 DTO MODEL 映射服务
        /// </summary>
        [Import]
        IDtoMapService DtoMap { get; set; }
        /// <summary>
        /// 获取或设置 角色信息校验对象
        /// </summary>
        [Import]
        IValidator<SysRole> Validator { get; set; }
        #endregion

        #region //// 标准曾删改服务接口实现
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> CreateAsync(DTORole entity)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 实体模型转换
            var role = DtoMap.Map<SysRole>(entity);
            // 校验实体
            var validateResult = await Validator.ValidateAsync(role);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                // 添加到实体集合中
                Repository.Insert(role, AutoSaved);
                // 返回正确
                return new OperationResult(OperationResultType.Success);
            }
            catch(DataAccessException ex)
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
            var role = Repository.Entities.SingleOrDefault(d => d.Name.Equals(name));
            if (role == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.RoleNotExist
                    , name)));
            try
            {
                // 从实体集合删除
                Repository.Delete(role.Id, AutoSaved);
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
        public virtual async Task<OperationResult> UpdateAsync(DTORole entity)
        {
            /// 更新角色信息
            /// 考虑是否需要限制更改角色名称
            /// 其他更新信息同样存在这个问题
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 获取用户
            var role = Repository.Entities.SingleOrDefault(m => m.Name.Equals(entity.Name));
            if (role == null)
                return new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.RoleNotExist
                    , entity.Name));
            // 实体模型转换
            role = DtoMap.Map<DTORole, SysRole>(entity, role);
            // 校验实体        
            var validateResult = await Validator.ValidateAsync(role);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                //更新实体
                Repository.Update(role, AutoSaved);
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
        /// 按照名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual Task<DTORole> FindByNameAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var role = Repository.Entities.SingleOrDefault(m => m.Name.Equals(name));
            if (role == null)
            {
                return Task.FromResult<DTORole>(null);
            }
            return Task.FromResult<DTORole>(DtoMap.Map<DTORole>(role));
        }

        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTORole>> PageListAsync(int skip, int limit, string order)
        {
            PublicHelper.CheckArgument(order, "Order");
            PublicHelper.CheckArgument(skip, "Skip", true);
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
                // 获取排序查询
                var query = Repository.Entities.OrderBy(order);
                // 获取分页数据
                var users = query.Skip(skip).Take(limit).ToList();
                // 模型转换        
                return Task.FromResult<IEnumerable<DTORole>>(DtoMap.Map<IEnumerable<DTORole>>(users));
            }
            catch (DataAccessException ex)
            {
                throw PublicHelper.ThrowBusinessException(ex.Message, ex);
            }
        }
    }
}