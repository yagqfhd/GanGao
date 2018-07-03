using GanGao.IBLL.Systems;
using GanGao.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GanGao.API.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : ApiController
    {
        //public UserController()
        //{
        //    //RegisgterMEF.regisgter().ComposeParts(this);
        //}
        [Import]
        IUserService userService { get; set; }

        public IHttpActionResult Get()
        {
            return Ok(string.Format("测试 {0}: userService Import =[{1}]",new Random(100).Next(), userService==null));
        }
    }
}
