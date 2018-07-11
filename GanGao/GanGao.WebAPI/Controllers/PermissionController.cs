using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using GanGao.IBLL.Systems;
using GanGao.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GanGao.WebAPI.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/Permission")]
    public class PermissionController : APIBaseController
    {
        #region /// 属性定义
        ///IOC获取权限服务        
        [Import]
        IPermissionService Service { get; set; }
        #endregion

        #region /// 获取一个信息
        /// <summary>
        /// 获取一个信息 
        /// 访问方法 
        /// </summary>
        /// <param name="id">权限名</param>
        /// <returns>权限信息</returns>
        [Route("one"), HttpGet]
        [Description("获取一个权限信息")]
        public async Task<IHttpActionResult> one(string id)
        {
            if (string.IsNullOrWhiteSpace(id) == true) // 获取列表
            {
                return BadRequest(
                    String.Format(CultureInfo.CurrentCulture,
                    Resources.ParaError));
            }
            var DtoPermission = await Service.FindByNameAsync(id);
            if (DtoPermission == null) return BadRequest(
                String.Format(CultureInfo.CurrentCulture,
                Resources.EntityNotExist, "权限", id));
            return Ok(DtoPermission);
        }
        #endregion

        #region /// 获取分页列表
        [Route("page"), HttpPost] // {index:int}/{page:int}
        [Description("权限分页列表")]
        public async Task<IHttpActionResult> page([FromBody]PageInput page)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            var skip = (page.page - 1) * page.limit;
            Console.WriteLine("SKip={0},Limit={1}", skip, page.limit);
            try
            {
                var DtoPermissions = await Service.PageListAsync(skip, page.limit, "Name");
                return Ok(DtoPermissions);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 添加
        [Route("add"), HttpPost]
        [Description("添加权限")]
        public async Task<IHttpActionResult> add([FromBody] DTOPermission permission)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            try
            {
                // 调用服务创建
                var result = await Service.CreateAsync(permission);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ComponentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 更新
        [Route("update"), HttpPost]
        [Description("修改权限")]
        public async Task<IHttpActionResult> update([FromBody] DTOPermission permission)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务更新
                var result = await Service.UpdateAsync(permission);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ComponentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 删除
        [Route("remove"), HttpPost]
        [Description("删除权限")]
        public async Task<IHttpActionResult> remove([FromBody] DTOPermission permission)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务创建用户
                var result = await Service.DeleteAsync(permission.Name);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ComponentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 删除
        [Route("delete"), HttpGet]
        [Description("删除权限")]
        public async Task<IHttpActionResult> remove(string name)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion      
            try
            {
                // 调用服务创建用户
                var result = await Service.DeleteAsync(name);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (ComponentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 部门管理
        [Route("Department/Add"), HttpPost]
        [Description("添加到部门")]
        public async Task<IHttpActionResult> addDepartment([FromBody] PermissionDepartmentInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion 
            try
            {
                // 调用服务创建用户
                var result = await Service.AddDepartmentAsync(add.PermissionName, add.DepartmentName);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (DataAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Department/Remove"), HttpPost]
        [Description("从部门移除")]
        public async Task<IHttpActionResult> removeDepartment([FromBody] PermissionDepartmentInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务创建用户
                var result = await Service.RemoveDepartmentAsync(add.PermissionName, add.DepartmentName);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (DataAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        #endregion

        #region //// 部门角色管理
        [Route("Role/Add"), HttpPost]
        [Description("所在部门添加角色")]
        public async Task<IHttpActionResult> addRole([FromBody] PermissionRoleInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务创建用户
                var result = await Service.AddRoleAsync(add.PermissionName, add.DepartmentName, add.RoleName);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (DataAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Role/Remove"), HttpPost]
        [Description("所在部门移除角色")]
        public async Task<IHttpActionResult> removeRole([FromBody] PermissionRoleInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion        
            try
            {
                // 调用服务创建用户
                var result = await Service.RemoveRoleAsync(add.PermissionName, add.DepartmentName, add.RoleName);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (DataAccessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
