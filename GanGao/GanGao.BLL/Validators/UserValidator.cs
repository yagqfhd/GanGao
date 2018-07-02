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

        public UserValidator():base()
        {
            /// MEF IoC映射
            RegisgterMEF.regisgter().ComposeParts(this);
        }
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
            //检查用户名和Email
            var user = UserRepository.Entities.Where(u => u.Name.Equals(item.Name) || u.Email.Equals(item.Email)).FirstOrDefault();
            if (user != null && !EqualityComparer<string>.Default.Equals(user.Id, item.Id))
                return new OperationResult(OperationResultType.Failed,String.Format(CultureInfo.CurrentCulture, "{0}:重名错误", item.Name));
            return await base.ValidateAsync(item);
        }
    }
}