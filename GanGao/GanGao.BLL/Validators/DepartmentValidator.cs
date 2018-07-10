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
    ///     校验实现——部门信息名称重名检查
    /// </summary>
    [Export(typeof(IValidator<SysDepartment>))]
    public class DepartmentValidator : CoreValidator<SysDepartment>, IValidator<SysDepartment>
    {
        /// <summary>
        /// 获取或设置 数据访问对象
        /// </summary>
        [Import]
        protected IDepartmentRepository DepartmentRepository { get; set; }

        /// <summary>
        /// 校验方法重写
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<OperationResult> ValidateAsync(SysDepartment item)
        {
            //检查部门名重复性
            var Department = DepartmentRepository.Entities.Where(
                u => u.Name.Equals(item.Name))
                .FirstOrDefault();
            if (Department != null && !EqualityComparer<string>.Default.Equals(Department.Id, item.Id))
            {
                return new OperationResult(OperationResultType.Failed, 
                    String.Format(CultureInfo.CurrentCulture, 
                    SysResources.DuplicationName,"部门",
                    item.Name));
            }                
            return await base.ValidateAsync(item);
        }
    }
}