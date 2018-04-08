using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.Entitys.Parks;
using Park.Parks.Park.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Park
{
    public interface IParkAppService:IAsyncCrudAppService<CreateParkDto, int, PagedResultRequestDto,CreateParkDto, CreateParkDto>,IApplicationService
    {

    }
}
