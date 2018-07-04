﻿using GanGao.IBLL.Systems;
using GanGao.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GanGao.API.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : ApiController
    {
        ///IOC获取用户服务        
        [Import]
        IUserService userService { get; set; }


        public async Task<IHttpActionResult> Get()
        {
            var username = await userService.FindUserAsync("admin");
            return Ok(string.Format("测试 {0}: userService Import =[{1}] userName =[{2}]",new Random().Next(1,1000), userService==null,username==null? "无用户":username.Name));
        }
    }
}
