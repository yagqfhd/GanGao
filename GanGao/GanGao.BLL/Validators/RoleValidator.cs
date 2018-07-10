using GanGao.BLL.Systems;
using GanGao.Common;
using GanGao.Common.Model.Systems;
using GanGao.IBLL.Systems;
using GanGao.IDAL.ISystems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GanGao.BLL.Validators
{
    /// <summary>
    ///     校验实现——角色信息名称重名检查
    /// </summary>
    [Export(typeof(IValidator<SysRole>))]
    public class RoleValidator : CoreValidator<SysRole>, IValidator<SysRole>
    {
        /// <summary>
        /// 获取或设置 信息数据访问对象
        /// </summary>
        [Import]
        protected IRoleRepository RoleRepository { get; set; }

        /// <summary>
        /// 校验方法重写
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<OperationResult> ValidateAsync(SysRole item)
        {
            //检查角色名重复性
            var role = RoleRepository.Entities.Where(
                u => u.Name.Equals(item.Name))
                .FirstOrDefault();
            if (role != null && !EqualityComparer<string>.Default.Equals(role.Id, item.Id))
            {
                return new OperationResult(OperationResultType.Failed, 
                    String.Format(CultureInfo.CurrentCulture, 
                    SysResources.DuplicationName,"角色",
                    item.Name));
            }                
            return await base.ValidateAsync(item);
        }
    }
}