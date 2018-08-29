
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

using Park.CarTypeses.Authorization;
using Park.CarTypeses.Dtos;
using Park.Entitys.CarTypes;

namespace Park.CarTypeses
{
    /// <summary>
    /// CarTypes应用层服务的接口实现方法  
    ///</summary>
//[AbpAuthorize(CarTypesAppPermissions.CarTypes)] 
    public class CarTypesAppService : ParkAppServiceBase, ICarTypesAppService
    {
    private readonly IRepository<CarTypes, long>
    _cartypesRepository;
    
       
       private readonly ICarTypesManager _cartypesManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public CarTypesAppService(
    IRepository<CarTypes, long>
cartypesRepository
        ,ICarTypesManager cartypesManager
        )
        {
        _cartypesRepository = cartypesRepository;
  _cartypesManager=cartypesManager;
        }


        /// <summary>
            /// 获取CarTypes的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CarTypesListDto>> GetPagedCarTypess(GetCarTypessInput input)
		{

		    var query = _cartypesRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var cartypesCount = await query.CountAsync();

			var cartypess = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var cartypesListDtos = ObjectMapper.Map<List <CarTypesListDto>>(cartypess);
				var cartypesListDtos =cartypess.MapTo<List<CarTypesListDto>>();

				return new PagedResultDto<CarTypesListDto>(
cartypesCount,
cartypesListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取CarTypesListDto信息
		/// </summary>
		public async Task<CarTypesListDto> GetCarTypesByIdAsync(EntityDto<long> input)
		{
			var entity = await _cartypesRepository.GetAsync(input.Id);

		    return entity.MapTo<CarTypesListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCarTypesForEditOutput> GetCarTypesForEdit(NullableIdDto<long> input)
		{
			var output = new GetCarTypesForEditOutput();
CarTypesEditDto cartypesEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _cartypesRepository.GetAsync(input.Id.Value);

cartypesEditDto = entity.MapTo<CarTypesEditDto>();

				//cartypesEditDto = ObjectMapper.Map<List <cartypesEditDto>>(entity);
			}
			else
			{
cartypesEditDto = new CarTypesEditDto();
			}

			output.CarTypes = cartypesEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CarTypes的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateCarTypes(CreateOrUpdateCarTypesInput input)
		{

			if (input.CarTypes.Id.HasValue)
			{
				await UpdateCarTypesAsync(input.CarTypes);
			}
			else
			{
				await CreateCarTypesAsync(input.CarTypes);
			}
		}


		/// <summary>
		/// 新增CarTypes
		/// </summary>
		//[AbpAuthorize(CarTypesAppPermissions.CarTypes_Create)]
		protected virtual async Task<CarTypesEditDto> CreateCarTypesAsync(CarTypesEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CarTypes>(input);

			entity = await _cartypesRepository.InsertAsync(entity);
			return entity.MapTo<CarTypesEditDto>();
		}

		/// <summary>
		/// 编辑CarTypes
		/// </summary>
		//[AbpAuthorize(CarTypesAppPermissions.CarTypes_Edit)]
		protected virtual async Task UpdateCarTypesAsync(CarTypesEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _cartypesRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _cartypesRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CarTypes信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(CarTypesAppPermissions.CarTypes_Delete)]
		public async Task DeleteCarTypes(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _cartypesRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CarTypes的方法
		/// </summary>
		          //[AbpAuthorize(CarTypesAppPermissions.CarTypes_BatchDelete)]
		public async Task BatchDeleteCarTypessAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _cartypesRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<CarTypesListDto>> GetTypesListDtos()
        {
            var list = await _cartypesRepository.GetAll().ToListAsync();
            return list.Select(x => x.MapTo<CarTypesListDto>()).ToList();
        }

        /// <summary>
        /// 导出CarTypes为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetCarTypessToExcel()
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


