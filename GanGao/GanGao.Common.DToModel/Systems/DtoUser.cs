using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{
    /// <summary>
    /// DTO用户
    /// </summary>
    public class DTOUser
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 用户联系邮箱
        /// </summary>  
        public string Email { get; set; }
        /// <summary>
        /// 加密保存的密码
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 用户的真实姓名
        /// </summary>
        public string TrueName { get; set; }
    }
}