
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System.Collections.Generic;

namespace GanGao.Common.DToMap
{
    /// <summary>
    /// DTO模型映射到Model的服务接口定义
    /// </summary>
    public interface IDtoMapService
    {
        /// <summary>
        /// 用户的 DTO Model 映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DTOUser UserDtoM(SysUser user,DTOUser dto=null);
        /// <summary>
        /// 用户的 DTO Model 映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        SysUser UserMtoD(DTOUser dto,SysUser user=null);
        /// <summary>
        /// 用户的 DTO Model 映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<DTOUser> UserDtoM(IEnumerable<SysUser> user);
        
    }
}
