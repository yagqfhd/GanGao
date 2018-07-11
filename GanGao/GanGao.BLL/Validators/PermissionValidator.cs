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
using System.Web;

namespace GanGao.BLL.Validators
{
    /// <summary>
    ///     校验实现——权限信息检查
    /// </summary>
    [Export(typeof(IValidator<SysPermission>))]
    public class PermissionValidator : CoreValidator<SysPermission>, IValidator<SysPermission>
    {
        /// <summary>
        /// 获取或设置 数据访问对象
        /// </summary>
        [Import]
        protected IPermissionRepository Repository { get; set; }

        /// <summary>
        /// 校验方法重写
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<OperationResult> ValidateAsync(SysPermission item)
        {
            //检查角色名重复性
            var permission = Repository.Entities.Where(
                u => u.Name.Equals(item.Name))
                .FirstOrDefault();
            if (permission != null && !EqualityComparer<string>.Default.Equals(permission.Id, item.Id))
            {
                return new OperationResult(OperationResultType.Failed,
                    String.Format(CultureInfo.CurrentCulture,
                    SysResources.DuplicationName, "权限",
                    item.Name));
            }
            return await base.ValidateAsync(item);
        }
    }
}