using GanGao.Common;
using System.Threading.Tasks;

namespace GanGao.BLL.Validators
{
    /// <summary>
    /// 核心业务数据校验实现基类
    /// </summary>
    public abstract class CoreValidator<TEntity>
    {
        public virtual Task<OperationResult> ValidateAsync(TEntity item)
        {
            return Task.FromResult<OperationResult>(new OperationResult(OperationResultType.Success));
        }
    }
}