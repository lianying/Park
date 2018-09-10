
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

using Park.CarUserses.Authorization;
using Park.CarUserses.Dtos;
using Park.Entitys.CarUsers;
using Park.CarPorts;
using Park.CarPorts.Dtos;

namespace Park.CarUserses
{
    /// <summary>
    /// CarUsers应用层服务的接口实现方法  
    ///</summary>
//[AbpAuthorize(CarUsersAppPermissions.CarUsers)] 
    public class CarUsersAppService : ParkAppServiceBase, ICarUsersAppService
    {
    private readonly IRepository<CarUsers, long>
    _carusersRepository;
    
       
       private readonly ICarUsersManager _carusersManager;
        private readonly ICarPortAppService _carPortAppService;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public CarUsersAppService(
    IRepository<CarUsers, long>
carusersRepository
        ,ICarUsersManager carusersManager,
    ICarPortAppService carPortAppService
        )
        {
        _carusersRepository = carusersRepository;
  _carusersManager=carusersManager;
            _carPortAppService = carPortAppService;
        }


        /// <summary>
            /// 获取CarUsers的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CarUsersListDto>> GetPagedCarUserss(GetCarUserssInput input)
		{

		    var query = _carusersRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var carusersCount = await query.CountAsync();

			var caruserss = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var carusersListDtos = ObjectMapper.Map<List <CarUsersListDto>>(caruserss);
				var carusersListDtos =caruserss.MapTo<List<CarUsersListDto>>();

				return new PagedResultDto<CarUsersListDto>(
carusersCount,
carusersListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取CarUsersListDto信息
		/// </summary>
		public async Task<CarUsersListDto> GetCarUsersByIdAsync(EntityDto<long> input)
		{
			var entity = await _carusersRepository.GetAsync(input.Id);

		    return entity.MapTo<CarUsersListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCarUsersForEditOutput> GetCarUsersForEdit(NullableIdDto<long> input)
		{
			var output = new GetCarUsersForEditOutput();
CarUsersEditDto carusersEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _carusersRepository.GetAsync(input.Id.Value);

carusersEditDto = entity.MapTo<CarUsersEditDto>();

				//carusersEditDto = ObjectMapper.Map<List <carusersEditDto>>(entity);
			}
			else
			{
carusersEditDto = new CarUsersEditDto();
			}

			output.CarUsers = carusersEditDto;
			return output;
		}


        /// <summary>
        /// 添加或者修改CarUsers的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateCarUsers(CreateOrUpdateCarUsersInput input)
        {
            if (input.CarUsers.ParkId == 0)
            {
                input.CarUsers.ParkId = input.CarUsers.UserArea.ParkArea.Park.Id;
            }
            if (input.CarUsers.Id.HasValue && input.CarUsers.Id > 0)
            {
                await UpdateCarUsersAsync(input.CarUsers);
            }
            else
            {
                await CreateCarUsersAsync(input.CarUsers);
            }
        }


		/// <summary>
		/// 新增CarUsers
		/// </summary>
		//[AbpAuthorize(CarUsersAppPermissions.CarUsers_Create)]
		protected virtual async Task<CarUsersListDto> CreateCarUsersAsync(CarUsersListDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CarUsers>(input);
            entity.UserArea = null;
            entity.Park = null;
			entity = await _carusersRepository.InsertAsync(entity);
			return entity.MapTo<CarUsersListDto>();
		}

		/// <summary>
		/// 编辑CarUsers
		/// </summary>
		//[AbpAuthorize(CarUsersAppPermissions.CarUsers_Edit)]
		protected virtual async Task UpdateCarUsersAsync(CarUsersListDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _carusersRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);
			// ObjectMapper.Map(input, entity);
		    await _carusersRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CarUsers信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(CarUsersAppPermissions.CarUsers_Delete)]
		public async Task DeleteCarUsers(EntityDto<long> input)
		{
            //TODO:删除前的逻辑判断，是否允许删除
            var list = await _carPortAppService.GetCarPortListDtosByUserId(input.Id);
            list.ForEach(async x =>
            {
                x.CarUserId = null;
                x.CarUser = null;
                await _carPortAppService.CreateOrUpdateCarPort(new CarPorts.Dtos.CreateOrUpdateCarPortInput() { CarPort = x.MapTo<CarPortEditDto>() });

            });

			await _carusersRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CarUsers的方法
		/// </summary>
		          //[AbpAuthorize(CarUsersAppPermissions.CarUsers_BatchDelete)]
		public async Task BatchDeleteCarUserssAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _carusersRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<CarUsersListDto>> GetCarUsersListDtosByGroupId(int id)
        {
            var list = await _carusersRepository.GetAll().Where(X => X.GroupId == id).ToListAsync();
            return list.MapTo<List<CarUsersListDto>>();
        }

        /// <summary>
        /// 导出CarUsers为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetCarUserssToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes
        ///



        //// custom codes end

    }
}


