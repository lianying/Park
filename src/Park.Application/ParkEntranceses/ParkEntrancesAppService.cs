
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using  System.Data.Entity;
using System.Linq.Dynamic;

using Park.ParkEntranceses.Authorization;
using Park.ParkEntranceses.Dtos;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntranceses
{
    /// <summary>
    /// ParkEntrances应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(ParkEntrancesAppPermissions.ParkEntrances)] 
    public class ParkEntrancesAppService : ParkAppServiceBase, IParkEntrancesAppService
    {
    private readonly IRepository<ParkEntrances, long>
    _parkentrancesRepository;
    
       
       private readonly IParkEntrancesManager _parkentrancesManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public ParkEntrancesAppService(
    IRepository<ParkEntrances, long>
parkentrancesRepository
        ,IParkEntrancesManager parkentrancesManager
        )
        {
        _parkentrancesRepository = parkentrancesRepository;
  _parkentrancesManager=parkentrancesManager;
        }


        /// <summary>
            /// 获取ParkEntrances的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<ParkEntrancesListDto>> GetPagedParkEntrancess(GetParkEntrancessInput input)
		{

		    var query = _parkentrancesRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var parkentrancesCount = await query.CountAsync();

			var parkentrancess = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var parkentrancesListDtos = ObjectMapper.Map<List <ParkEntrancesListDto>>(parkentrancess);
				var parkentrancesListDtos =parkentrancess.MapTo<List<ParkEntrancesListDto>>();

				return new PagedResultDto<ParkEntrancesListDto>(
parkentrancesCount,
parkentrancesListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取ParkEntrancesListDto信息
		/// </summary>
		public async Task<ParkEntrancesListDto> GetParkEntrancesByIdAsync(EntityDto<long> input)
		{
			var entity = await _parkentrancesRepository.GetAsync(input.Id);

		    return entity.MapTo<ParkEntrancesListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetParkEntrancesForEditOutput> GetParkEntrancesForEdit(NullableIdDto<long> input)
		{
			var output = new GetParkEntrancesForEditOutput();
ParkEntrancesEditDto parkentrancesEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _parkentrancesRepository.GetAsync(input.Id.Value);

parkentrancesEditDto = entity.MapTo<ParkEntrancesEditDto>();

				//parkentrancesEditDto = ObjectMapper.Map<List <parkentrancesEditDto>>(entity);
			}
			else
			{
parkentrancesEditDto = new ParkEntrancesEditDto();
			}

			output.ParkEntrances = parkentrancesEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ParkEntrances的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateParkEntrances(CreateOrUpdateParkEntrancesInput input)
		{

			if (input.ParkEntrances.Id.HasValue)
			{
				await UpdateParkEntrancesAsync(input.ParkEntrances);
			}
			else
			{
				await CreateParkEntrancesAsync(input.ParkEntrances);
			}
		}


		/// <summary>
		/// 新增ParkEntrances
		/// </summary>
		[AbpAuthorize(ParkEntrancesAppPermissions.ParkEntrances_Create)]
		protected virtual async Task<ParkEntrancesEditDto> CreateParkEntrancesAsync(ParkEntrancesEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <ParkEntrances>(input);

			entity = await _parkentrancesRepository.InsertAsync(entity);
			return entity.MapTo<ParkEntrancesEditDto>();
		}

		/// <summary>
		/// 编辑ParkEntrances
		/// </summary>
		[AbpAuthorize(ParkEntrancesAppPermissions.ParkEntrances_Edit)]
		protected virtual async Task UpdateParkEntrancesAsync(ParkEntrancesEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _parkentrancesRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _parkentrancesRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ParkEntrances信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(ParkEntrancesAppPermissions.ParkEntrances_Delete)]
		public async Task DeleteParkEntrances(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _parkentrancesRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ParkEntrances的方法
		/// </summary>
		          [AbpAuthorize(ParkEntrancesAppPermissions.ParkEntrances_BatchDelete)]
		public async Task BatchDeleteParkEntrancessAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _parkentrancesRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出ParkEntrances为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetParkEntrancessToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}



		//// custom codes
		 
        //// custom codes end

    }
}


