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
    /// 部门服务层
    /// </summary>
    [Export(typeof(IDepartmentService))]
    public class DepartmentService : CoreServiceBase, IDepartmentService
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
        protected IDepartmentRepository Repository { get; set; }
        /// <summary>
        /// 获取或设置 DTO MODEL 映射服务
        /// </summary>
        [Import]
        IDtoMapService DtoMap { get; set; }
        /// <summary>
        /// 获取或设置 角色信息校验对象
        /// </summary>
        [Import]
        IValidator<SysDepartment> Validator { get; set; }
        #endregion

        #region //// 标准曾删改服务接口实现
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> CreateAsync(DTODepartment entity)
        {
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 实体模型转换
            var Department = DtoMap.Map<SysDepartment>(entity);
            // 校验实体
            var validateResult = await Validator.ValidateAsync(Department);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                // 添加到实体集合中
                Repository.Insert(Department, AutoSaved);
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
            var Department = Repository.Entities.SingleOrDefault(d => d.Name.Equals(name));
            if (Department == null)
                return Task.FromResult<OperationResult>(
                    new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , name)));
            try
            {
                // 从实体集合删除
                Repository.Delete(Department.Id, AutoSaved);
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
        public virtual async Task<OperationResult> UpdateAsync(DTODepartment entity)
        {
            /// 更新角色信息
            /// 考虑是否需要限制更改角色名称
            /// 其他更新信息同样存在这个问题
            //校验参数！=NULL
            PublicHelper.CheckArgument(entity, "entity");
            // 获取用户
            var Department = Repository.Entities.SingleOrDefault(m => m.Name.Equals(entity.Name));
            if (Department == null)
                return new OperationResult(OperationResultType.Warning,
                    String.Format(CultureInfo.CurrentCulture,
                    Systems.SysResources.DepartmentNotExist
                    , entity.Name));
            // 实体模型转换
            Department = DtoMap.Map<DTODepartment, SysDepartment>(entity, Department);
            // 校验实体        
            var validateResult = await Validator.ValidateAsync(Department);
            if (validateResult.ResultType != OperationResultType.Success)
                return validateResult;
            try
            {
                //更新实体
                Repository.Update(Department, AutoSaved);
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
        public virtual Task<DTODepartment> FindByNameAsync(string name)
        {
            PublicHelper.CheckArgument(name, "name");
            var role = Repository.Entities.SingleOrDefault(m => m.Name.Equals(name));
            if (role == null)
            {
                return Task.FromResult<DTODepartment>(null);
            }
            return Task.FromResult<DTODepartment>(DtoMap.Map<DTODepartment>(role));
        }

        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<DTODepartment>> PageListAsync(int skip, int limit, string order,string parent = null)
        {
            PublicHelper.CheckArgument(order, "Order");
            PublicHelper.CheckArgument(skip, "Skip", true);
            PublicHelper.CheckArgument(limit, "Limit");

            var queryAll = Repository.Entities;
            if (string.IsNullOrWhiteSpace(parent) == false)
                queryAll = queryAll.Where(d => d.Parent.Name.Equals(parent));
            else
                queryAll = queryAll.Where(d => d.Parent == null);
            //获取记录数
            var allCount = queryAll.Count();
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
                var query = queryAll.OrderBy(order);
                // 获取分页数据
                var users = query.Skip(skip).Take(limit).ToList();
                // 模型转换        
                return Task.FromResult<IEnumerable<DTODepartment>>(DtoMap.Map<IEnumerable<DTODepartment>>(users));
            }
            catch (DataAccessException ex)
            {
                throw PublicHelper.ThrowBusinessException(ex.Message, ex);
            }
        }

        
        /// <summary>
        /// 设置部门的上级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public virtual Task<OperationResult> SetParent(string name, string parentName)
        {
            ///
            /// 设置上级部门，会自动将原来的上级部门取消
            /// 可以考虑加个参数控制
            ///检查参数
            PublicHelper.CheckArgument(name, "name");
            PublicHelper.CheckArgument(parentName, "parentName");
            try
            {
                // 获取部门
                var department = Repository.Entities.FirstOrDefault(d => d.Name.Equals(name));
                if (department == null)
                    return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull,
                        String.Format(CultureInfo.CurrentCulture,
                        Systems.SysResources.DepartmentNotExist,
                        name)));
                // 获取上级部门
                var departmentParent = Repository.Entities.FirstOrDefault(d => d.Name.Equals(parentName));
                if (departmentParent == null)
                    return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull,
                        String.Format(CultureInfo.CurrentCulture,
                        Systems.SysResources.DepartmentNotExist,
                        name)));
                // 检查原上级部门是否存在并不是要设置的上级部门
                if (department.Parent != null && department.Parent.Id == departmentParent.Id)
                {
                    return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.QueryNull,
                        String.Format(CultureInfo.CurrentCulture,
                        Systems.SysResources.DepartmentParentIsExist,
                        name, parentName)));
                }
                department.Parent = departmentParent;
                Repository.Update(department, AutoSaved);
                return Task.FromResult(new OperationResult(OperationResultType.Success));
            }
            catch(DataAccessException ex)
            {
                return Task.FromResult(new OperationResult(OperationResultType.Error, ex.Message));
            }
        }
        /// <summary>
        /// 添加部门的下级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public virtual  Task<OperationResult> AddChild(string name, string childName)
        {
            ///调换父子关系
            return this.SetParent(childName, name);
        }
        /// <summary>
        /// 添加部门的下级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult> AddChilds(string name, IEnumerable<string> childNames)
        {
            var result = new OperationResult(OperationResultType.Success);
            foreach(var childName in childNames)
            {
                var ret = await this.SetParent(childName, name);
                if(ret.ResultType != OperationResultType.Success)
                {
                    result.ResultType = ret.ResultType;
                    
                }
                result.Message += ret.Message;
            }
            return result;
        }
    }
}