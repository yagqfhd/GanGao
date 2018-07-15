using GanGao.BLL.Systems;
using GanGao.Common;
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
                Console.WriteLine("UserValidator:Users={0}", users.Count());
                foreach(var user in users)
                {
                    if(!EqualityComparer<string>.Default.Equals(user.Id, item.Id))
                    {
                        Console.WriteLine("user.Id={0}, item.Id={1}", user.Id, item.Id);
                        Console.WriteLine("user.Name={0}, item.Name={1}", user.Name, item.Name);
                        Console.WriteLine("user.Email={0}, item.Email={1}", user.Email, item.Email);
                        Console.WriteLine("user.Nick={0}, item.Nick={1}", user.Nick, item.Nick);
                        if (user.Name.Equals(item.Name))
                            return new OperationResult(OperationResultType.Failed,
                                String.Format(CultureInfo.CurrentCulture,
                                SysResources.DuplicationName, "角色",
                                item.Name));
                        else if (user.Email.Equals(item.Email))
                            return new OperationResult(OperationResultType.Failed, 
                                String.Format(CultureInfo.CurrentCulture,
                                SysResources.DuplicationName,"用户Email", item.Email));
                        else if (user.Nick.Equals(item.Nick))
                            return new OperationResult(OperationResultType.Failed, 
                                String.Format(CultureInfo.CurrentCulture,
                                SysResources.DuplicationName,"昵称", item.Nick));
                        else
                            return new OperationResult(OperationResultType.Failed, 
                                String.Format(CultureInfo.CurrentCulture,
                                SysResources.DuplicationName,"真实姓名", item.TrueName));
                    }
                }
                
            }                
            return await base.ValidateAsync(item);
        }
    }
}