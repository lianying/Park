using Abp.Application.Services;
using Abp.Domain.Repositories;
using Park.Entitys.Parks;
using Park.Parks.Park.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Park.Parks.Park
{
    public class ParkAppService:AsyncCrudAppService<ParkSet, CreateParkDto,int, PagedResultRequestDto,CreateParkDto,CreateParkDto>,IParkAppService
    {
        //readonly IRepository<JinQuPark> _repository;
        public ParkAppService(IRepository<ParkSet> repository) : base(repository)
        {
            //_repository = repository;

            
        }

        public override Task<PagedResultDto<CreateParkDto>> GetAll(PagedResultRequestDto input)
        {

            return base.GetAll(input);
        }


    }
}
