
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

using Park.CarNumberses.Authorization;
using Park.CarNumberses.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarNumberses
{
    /// <summary>
    /// CarNumbers应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(CarNumbersAppPermissions.CarNumbers)] 
    public class CarNumbersAppService : ParkAppServiceBase, ICarNumbersAppService
    {
    private readonly IRepository<CarNumbers, long>
    _carnumbersRepository;
    
       
       private readonly ICarNumbersManager _carnumbersManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public CarNumbersAppService(
    IRepository<CarNumbers, long>
carnumbersRepository
        ,ICarNumbersManager carnumbersManager
        )
        {
        _carnumbersRepository = carnumbersRepository;
  _carnumbersManager=carnumbersManager;
        }


        /// <summary>
            /// 获取CarNumbers的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CarNumbersListDto>> GetPagedCarNumberss(GetCarNumberssInput input)
		{

		    var query = _carnumbersRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var carnumbersCount = await query.CountAsync();

			var carnumberss = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var carnumbersListDtos = ObjectMapper.Map<List <CarNumbersListDto>>(carnumberss);
				var carnumbersListDtos =carnumberss.MapTo<List<CarNumbersListDto>>();

				return new PagedResultDto<CarNumbersListDto>(
carnumbersCount,
carnumbersListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取CarNumbersListDto信息
		/// </summary>
		public async Task<CarNumbersListDto> GetCarNumbersByIdAsync(EntityDto<long> input)
		{
			var entity = await _carnumbersRepository.GetAsync(input.Id);

		    return entity.MapTo<CarNumbersListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCarNumbersForEditOutput> GetCarNumbersForEdit(NullableIdDto<long> input)
		{
			var output = new GetCarNumbersForEditOutput();
CarNumbersEditDto carnumbersEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _carnumbersRepository.GetAsync(input.Id.Value);

carnumbersEditDto = entity.MapTo<CarNumbersEditDto>();

				//carnumbersEditDto = ObjectMapper.Map<List <carnumbersEditDto>>(entity);
			}
			else
			{
carnumbersEditDto = new CarNumbersEditDto();
			}

			output.CarNumbers = carnumbersEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CarNumbers的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateCarNumbers(CreateOrUpdateCarNumbersInput input)
		{

			if (input.CarNumbers.Id.HasValue)
			{
				await UpdateCarNumbersAsync(input.CarNumbers);
			}
			else
			{
				await CreateCarNumbersAsync(input.CarNumbers);
			}
		}


		/// <summary>
		/// 新增CarNumbers
		/// </summary>
		[AbpAuthorize(CarNumbersAppPermissions.CarNumbers_Create)]
		protected virtual async Task<CarNumbersEditDto> CreateCarNumbersAsync(CarNumbersEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CarNumbers>(input);

			entity = await _carnumbersRepository.InsertAsync(entity);
			return entity.MapTo<CarNumbersEditDto>();
		}

		/// <summary>
		/// 编辑CarNumbers
		/// </summary>
		[AbpAuthorize(CarNumbersAppPermissions.CarNumbers_Edit)]
		protected virtual async Task UpdateCarNumbersAsync(CarNumbersEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _carnumbersRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _carnumbersRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CarNumbers信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(CarNumbersAppPermissions.CarNumbers_Delete)]
		public async Task DeleteCarNumbers(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _carnumbersRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CarNumbers的方法
		/// </summary>
		          [AbpAuthorize(CarNumbersAppPermissions.CarNumbers_BatchDelete)]
		public async Task BatchDeleteCarNumberssAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _carnumbersRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出CarNumbers为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetCarNumberssToExcel()
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


