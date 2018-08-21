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
using Park.ParkBox.Dto;

namespace Park.Parks.Park
{
    public class ParkAppService:AsyncCrudAppService<ParkSet, ParkDto,int, PagedResultRequestDto,CreateParkDto,CreateParkDto>,IParkAppService
    {
        readonly IRepository<ParkSet> _repository;
        public ParkAppService(IRepository<ParkSet> repository) : base(repository)
        {
            _repository = repository;
            
        }

        public override Task<PagedResultDto<ParkDto>> GetAll(PagedResultRequestDto input)
        {

            return base.GetAll(input);
        }

        public override Task<ParkDto> Create(CreateParkDto input)
        {
            return base.Create(input);
        }

        public async Task<PagedResultDto<ParkDto>> GetParkListByName(PagedResultRequestDto input,string Name)
        {
            if (Name.IsNullOrEmpty())
            {
                return await base.GetAll(input);
            }
            else
            {
                var list = _repository.GetAll().Where(x => x.Name.Contains(Name)).ToList();
                var result = new PagedResultDto<ParkDto>(list.Count, ObjectMapper.Map<List<ParkDto>>(list));
                return result;
            }

        }

     
    }
}
