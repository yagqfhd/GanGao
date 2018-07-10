using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using GanGao.IBLL.Systems;
using GanGao.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GanGao.WebAPI.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("Api/Role")]
    public class RoleController : APIBaseController
    {
        #region /// 属性定义
        ///IOC获取角色服务        
        [Import]
        IRoleService Service { get; set; }
        #endregion

        #region /// 获取一个信息
        /// <summary>
        /// 获取一个角色信息 
        /// 访问方法 
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>用户信息</returns>
        [Route("one"), HttpGet]
        public async Task<IHttpActionResult> one(string id)
        {
            if (string.IsNullOrWhiteSpace(id) == true) // 获取列表
            {
                return BadRequest(
                    String.Format(CultureInfo.CurrentCulture,
                    Resources.ParaError));
            }
            var DtoRole = await Service.FindByNameAsync(id);
            if (DtoRole == null) return BadRequest(
                String.Format(CultureInfo.CurrentCulture,
                Resources.EntityNotExist, "角色", id));
            return Ok(DtoRole);
        }
        #endregion

        #region /// 获取分页列表
        [Route("page"), HttpPost] // {index:int}/{page:int}
        public async Task<IHttpActionResult> page([FromBody]PageInput page)
        {
            var skip = (page.page - 1) * page.limit;
            try
            {
                var DtoRoles = await Service.PageListAsync(skip, page.limit, "Name");
                return Ok(DtoRoles);
            }
            catch (BusinessException ex)
            {                
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 添加
        [Route("add"), HttpPost]
        public async Task<IHttpActionResult> add([FromBody] DTORole role)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            try
            {
                // 调用服务创建
                var result = await Service.CreateAsync(role);
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
        public async Task<IHttpActionResult> update([FromBody] DTORole role)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务更新
                var result = await Service.UpdateAsync(role);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch(BusinessException ex)
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
        public async Task<IHttpActionResult> remove([FromBody] DTORole role)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务创建用户
                var result = await Service.DeleteAsync(role.Name);
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
        [Route("delete/{name}"), HttpPost]
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
    }
}
