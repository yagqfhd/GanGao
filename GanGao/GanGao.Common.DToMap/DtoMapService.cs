
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
        public IEnumerable<DTOUser> UserDtoM(IEnumerable<SysUser> user)
        {
            throw new NotImplementedException();
        }

        public DTOUser UserDtoM(SysUser user, DTOUser dto = null)
        {
            throw new NotImplementedException();
        }

        public SysUser UserMtoD(DTOUser dto, SysUser user = null)
        {
            throw new NotImplementedException();
        }
    }
}