﻿using GanGao.Common;
using GanGao.Common.Model.Systems;
using GanGao.IBLL.Systems;
using GanGao.IDAL.ISystems;
using GanGao.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GanGao.BLL.Validators
{
    /// <summary>
    ///     校验实现——用户信息名称重名检查
    /// </summary>
    [Export(typeof(IValidator<SysUser>))]
    public class UserValidator : CoreValidator<SysUser>, IValidator<SysUser>
    {
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected IUserRepository UserRepository { get; set; }

        /// <summary>
        /// 校验方法重写
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<OperationResult> ValidateAsync(SysUser item)
        {
            //检查用户名和Email , 昵称，姓名
            var users = UserRepository.Entities.Where(
                u => u.Name.Equals(item.Name) ||
                u.Email.Equals(item.Email) ||
                u.Nick.Equals(item.Nick) ||
                u.TrueName.Equals(item.TrueName));
            if (users != null)
            {
                foreach(var user in users)
                {
                    if(!EqualityComparer<string>.Default.Equals(user.Id, item.Id))
                    {
                        if (user.Name.Equals(item.Name))
                            return new OperationResult(OperationResultType.Failed, String.Format(CultureInfo.CurrentCulture, "{0}:用户名重名错误", item.Name));
                        else if (user.Email.Equals(item.Email))
                            return new OperationResult(OperationResultType.Failed, String.Format(CultureInfo.CurrentCulture, "{0}:Email重复错误", item.Email));
                        else if (user.Nick.Equals(item.Nick))
                            return new OperationResult(OperationResultType.Failed, String.Format(CultureInfo.CurrentCulture, "{0}:昵称重名错误", item.Nick));
                        else
                            return new OperationResult(OperationResultType.Failed, String.Format(CultureInfo.CurrentCulture, "{0}:姓名重名错误", item.TrueName));
                    }
                }
                
            }                
            return await base.ValidateAsync(item);
        }
    }
}