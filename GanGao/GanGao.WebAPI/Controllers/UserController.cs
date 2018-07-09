using GanGao.IBLL.Systems;
using GanGao.MEF;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using GanGao.WebAPI.Models;
using GanGao.Common.DToModel.Systems;

namespace GanGao.WebAPI.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/user")]
    public class UserController : APIBaseController
    {
        #region ///// 用户控制器初始化
//#if DEBUG
//        public UserController() : base()
//        {
//            ///IOC 
//            if (userService == null) RegisgterMEF.regisgter().ComposeParts(this);

//        }
//#endif
        #endregion

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
                return BadRequest("参数错误");
            }
            var DtoUser = await userService.FindUserAsync(id);
            if (DtoUser == null) return BadRequest("用户不存在");
            return Ok(DtoUser);            
        }
        #endregion

        #region /// 获取用户分页列表
        [Route("page"), HttpPost] // {index:int}/{page:int}
        public async Task<IHttpActionResult> page([FromBody]PageInput page)
        {
            
#if DEBUG
        Console.WriteLine("User/Get userService={0}", userService == null);
            if (userService == null)
            {
                return Ok("UserServer Map Error");
            }
            //[FromUri]string username
#endif 
            var skip = (page.page - 1) * page.limit;
            var DtoUsers = await userService.UserPageListAsync(skip, page.limit, "Name");
            if (DtoUsers == null || DtoUsers.Any() == false) return BadRequest("分页信息错误");
            return Ok(DtoUsers);
        }
        #endregion

        #region //// 添加用户
        public async Task<IHttpActionResult> add([FromBody] DTOUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(this.GetModelStateError(ModelState));
            return Ok();
        }
        #endregion
    }
}
