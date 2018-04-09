using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.Authorization.Permissions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Authorization.Permissions
{
    interface IPermissionAppService:IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
