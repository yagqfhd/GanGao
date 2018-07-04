
using System;
using System.Collections.Generic;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System.ComponentModel.Composition;

namespace GanGao.Common.DToMap
{
    [Export(typeof(IDtoMapService))]
    public class DtoMapService : IDtoMapService
    {
        public IEnumerable<DTOUser> DToToUser(IEnumerable<SysUser> user)
        {
            throw new NotImplementedException();
        }

        public DTOUser UserToDTo(SysUser user, DTOUser dto = null)
        {
            throw new NotImplementedException();
        }

        public SysUser DToToUser(DTOUser dto, SysUser user = null)
        {
            throw new NotImplementedException();
        }
    }
}