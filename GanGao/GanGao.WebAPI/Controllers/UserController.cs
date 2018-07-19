using GanGao.IBLL.Systems;
using GanGao.MEF;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using GanGao.WebAPI.Models;
using GanGao.Common.DToModel.Systems;
using System.Globalization;
using GanGao.Common;
using System.ComponentModel;

namespace GanGao.WebAPI.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/user")]
    public class UserController : APIBaseController
    {
        #region /// 属性定义
        ///IOC获取用户服务        
        [Import]
        IUserService userService { get; set; }
        #endregion

        #region /// 获取一个用户信息
        /// <summary>
        /// 获取一个用户信息 
        /// 访问方法 http://host/api/user/one?id=用户名
        /// </summary>
        /// <param name="id">用户名</param>
        /// <returns>用户信息</returns>
        [Route("one"),HttpGet]
        [Description("获取一个用户信息")]
        public async Task<IHttpActionResult> one(string id)
        {
#if DEBUG
            Console.WriteLine("Id={0}", id==null?"NOT":id);
            Console.WriteLine("User/Get userService={0}", userService == null);
            if (userService == null) RegisgterMEF.regisgter().ComposeParts(this);
            if (userService == null)
            {
                return Ok("UserServer Map Error");
            }
            //[FromUri]string username
            string username = id;
#endif 
            if (string.IsNullOrWhiteSpace(id) == true) // 获取列表
            {
                return BadRequest(
                    String.Format(CultureInfo.CurrentCulture,
                    Resources.ParaError));
            }
            var DtoUser = await userService.FindUserAsync(id);
            if (DtoUser == null) return BadRequest(
                String.Format(CultureInfo.CurrentCulture,
                Resources.EntityNotExist,"用户",id));
            return Ok(DtoUser);            
        }
        #endregion

        #region /// 获取用户分页列表
        [Description("用户分页列表")]
        [Route("page"), HttpPost] // {index:int}/{page:int}
        public async Task<IHttpActionResult> page([FromBody]PageInput page)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
#if DEBUG
            Console.WriteLine("User/page 用户分页列表userService={0}", userService == null);
            if (userService == null)
            {
                return BadRequest("UserServer Map Error");
            }
            //[FromUri]string username
#endif 
            var skip = (page.page - 1) * page.limit;
            try
            {
                var DtoUsers = await userService.UserPageListAsync(skip, page.limit, "Name");
                return Ok(DtoUsers);
            }
            catch (BusinessException ex)
            {
                Console.WriteLine("UserControll Get BusinessException Ex");
                return BadRequest(ex.Message);
            }            
        }
        #endregion

        #region //// 添加用户
        [Description("添加用户")]
        [Route("add"),HttpPost]
        public async Task<IHttpActionResult> add([FromBody] DTOUser user)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
            //OperationResult result;
#if DEBUG
            Console.WriteLine("User/add userService={0}", userService == null);
            if (userService == null)
            {
                return BadRequest("UserServer Map Error");
            }
            //[FromUri]string username
#endif 
            try
            {
                // 调用服务创建用户
                var result = await userService.CreateAsync(user);
                // 根据服务返回值确定返回
                if (result.ResultType == Common.OperationResultType.Success)
                    return Ok(true);
                return BadRequest(result.Message);
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);

            }
            catch(ComponentException ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        #endregion

        #region //// 更新用户
        [Route("update"), HttpPost]
        [Description("修改用户")]
        public async Task<IHttpActionResult> update([FromBody] DTOUser user)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion
#if DEBUG
            Console.WriteLine("User/update userService={0}", userService == null);
            if (userService == null)
            {
                return BadRequest("UserServer Map Error");
            }
            //[FromUri]string username
#endif 
            // 调用服务创建用户
            var result = await userService.UpdateAsync(user);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }
        #endregion

        #region //// 删除用户
        [Route("remove"), HttpPost]
        [Description("删除用户")]
        public async Task<IHttpActionResult> remove([FromBody] DTOUser user)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.DeleteAsync(user.Name);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }
        #endregion

        #region //// 删除用户
        [Route("delete/{username}"), HttpPost]
        [Description("删除用户")]
        public async Task<IHttpActionResult> remove(string username)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.DeleteAsync(username);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }
        #endregion

        #region //// 用户部门管理
        [Route("Department/Add"), HttpPost]
        [Description("用户添加到部门")]
        public async Task<IHttpActionResult> addDepartment([FromBody] UserDepartmentInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.AddDepartmentAsync(add.UserName, add.DepartmentName);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }

        [Route("Department/Remove"), HttpPost]
        [Description("用户从部门移除")]
        public async Task<IHttpActionResult> removeDepartment([FromBody] UserDepartmentInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.RemoveDepartmentAsync(add.UserName, add.DepartmentName);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }

        #endregion

        #region //// 用户部门角色管理
        [Route("Role/Add"), HttpPost]
        [Description("用户所在部门添加角色")]
        public async Task<IHttpActionResult> addRole([FromBody] UserRoleInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.AddRoleAsync(add.UserName, add.DepartmentName,add.RoleName);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }

        [Route("Role/Remove"), HttpPost]
        [Description("用户所在部门移除角色")]
        public async Task<IHttpActionResult> removeRole([FromBody] UserRoleInput add)
        {
            #region /// 检查输入信息正确性
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            #endregion            
            // 调用服务创建用户
            var result = await userService.RemoveRoleAsync(add.UserName, add.DepartmentName,add.RoleName);
            // 根据服务返回值确定返回
            if (result.ResultType == Common.OperationResultType.Success)
                return Ok(true);
            return BadRequest(result.Message);
        }

        #endregion
    }
}
