using GanGao.IBLL.Systems;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;

namespace GanGao.WebAPI.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : ApiController
    {
        ///IOC获取用户服务        
        [Import]
        IUserService userService { get; set; }


        public async Task<IHttpActionResult> Get(string username)
        {
            if (string.IsNullOrWhiteSpace(username) == true)
                return BadRequest("用户不存在");
            var DtoUser = await userService.FindUserAsync(username);
            var up = DtoUser;
            userService.AutoSaved = true;
            DtoUser.TrueName = "管理员";
            var result = await userService.UpdateAsync(DtoUser);
            return Ok( new { up, DtoUser });
            //return Ok(string.Format("测试 {0}: userService Import =[{1}] userName =[{2}]",new Random().Next(1,1000), userService==null,DtoUser==null? "无用户":DtoUser.Name));
        }
    }
}
