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
    /// 部门管理
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("Api/Department")]
    public class DepartmentController : APIBaseController
    {
        #region /// 属性定义
        ///IOC获取角色服务        
        [Import]
        IDepartmentService Service { get; set; }
        #endregion

        #region /// 获取一个信息
        /// <summary>
        /// 获取一个信息 
        /// 访问方法 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>部门信息</returns>
        [Route("one"), HttpGet]
        [Description("获取一个部门信息")]
        public virtual async Task<IHttpActionResult> one(string id)
        {
            if (string.IsNullOrWhiteSpace(id) == true) // 获取列表
            {
                return BadRequest(
                    String.Format(CultureInfo.CurrentCulture,
                    Resources.ParaError));
            }
            var DtoDepartment = await Service.FindByNameAsync(id);
            if (DtoDepartment == null) return BadRequest(
                String.Format(CultureInfo.CurrentCulture,
                Resources.EntityNotExist, "部门", id));
            return Ok(DtoDepartment);
        }
        #endregion

        #region /// 获取分页列表
        [Route("page"), HttpPost] // {index:int}/{page:int}
        [Description("获取部门分页列表")]
        public virtual async Task<IHttpActionResult> page([FromBody]DepartmentPageInput page)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            var skip = (page.page - 1) * page.limit; 
            Console.WriteLine("SKip={0},Limit={1},Parent={2}isnull{3}", skip, page.limit,page.Parent,page.Parent ==null);
            try
            {
                var DtoDepartments = await Service.PageListAsync(skip, page.limit, "Name",page.Parent);
                return Ok(DtoDepartments);
            }
            catch (BusinessException ex)
            {                
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region //// 添加
        [Route("add"), HttpPost]
        [Description("添加部门")]
        public async Task<IHttpActionResult> add([FromBody] DTODepartment Department)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            try
            {
                // 调用服务创建
                var result = await Service.CreateAsync(Department);
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
        [Description("修改部门")]
        public async Task<IHttpActionResult> update([FromBody] DTODepartment Department)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务更新
                var result = await Service.UpdateAsync(Department);
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
        [Description("删除部门")]
        public async Task<IHttpActionResult> remove([FromBody] DTODepartment Department)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                // 调用服务创建用户
                var result = await Service.DeleteAsync(Department.Name);
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
        [Description("删除部门")]
        public virtual async Task<IHttpActionResult> remove(string name)
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

        #region ////// 设置上级部门
        [Route("Parent"), HttpPost]
        [Description("设置上级部门")]
        public virtual async Task<IHttpActionResult> Parent([FromBody] DepartmentParentInput departmentParent)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                var result = await Service.SetParent(departmentParent.Child, departmentParent.Parent);
                return Ok(result.ResultType);
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

        #region /// 添加下级部门
        [Route("Child"), HttpPost]
        [Description("添加下级部门")]
        public virtual async Task<IHttpActionResult> Child([FromBody] DepartmentParentInput departmentParent)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            try
            {
                var result = await Service.SetParent(departmentParent.Parent, departmentParent.Child);
                return Ok(result.ResultType);
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
