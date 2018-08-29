
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
using System.Data.Entity;

using Park.CarPorts.Authorization;
using Park.CarPorts.Dtos;
using Park.Entitys.CarUsers;
using Park.Entitys.CarTypes;
using Park.CarTypeses;
using Park.CarTypeses.Dtos;
using Park.Entitys.ParkAreas;
using Park.ParkAreases.Dtos;
using Park.Entitys.ParkLevels;

namespace Park.CarPorts
{
    /// <summary>
    /// CarPort应用层服务的接口实现方法  
    ///</summary>
//[AbpAuthorize(CarPortAppPermissions.CarPort)] 
    public class CarPortAppService : ParkAppServiceBase, ICarPortAppService
    {
    private readonly IRepository<CarPort, long>
    _carportRepository;

        private readonly ICarTypesAppService _carTypesAppService;

        private readonly IRepository<ParkAreas, long>
   _parkAreaRepository;


        private readonly ICarPortManager _carportManager;
        private readonly IRepository<ParkLevels, long> _parkLevelsRepository;



    /// <summary>
    /// 构造函数 
    ///</summary>
        public CarPortAppService(
    IRepository<CarPort, long>
carportRepository
        ,ICarPortManager carportManager,
    ICarTypesAppService carTypesAppService,
    IRepository<ParkAreas, long> parkAreaRepository
        )
        {
        _carportRepository = carportRepository;
  _carportManager=carportManager;
            _carTypesAppService = carTypesAppService;
            _parkAreaRepository = parkAreaRepository;
        }


        /// <summary>
            /// 获取CarPort的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CarPortListDto>> GetPagedCarPorts(GetCarPortsInput input)
		{

		    var query = _carportRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var carportCount = await query.CountAsync();

            var carports = await query
                    .OrderBy((x) => input.Sorting)
                    .Include(x => x.CarUser)
                    .Include(x => x.CarPortType)
                    .Where(x => x.ParkId == input.ParkId)
                    .WhereIf(input.AreaId.HasValue && input.AreaId.Value > 0, x => x.AreaId == input.AreaId.Value)
                    .WhereIf(input.CarTypeId.HasValue && input.CarTypeId.Value > 0, x => x.CarPortType.Id == input.CarTypeId)
                    .WhereIf(!input.Filter.IsNullOrEmpty(), x => x.CarportSerial.Contains(input.Filter))
                    .AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

				// var carportListDtos = ObjectMapper.Map<List <CarPortListDto>>(carports);
				var carportListDtos =carports.MapTo<List<CarPortListDto>>();

            return new PagedResultDto<CarPortListDto>(
carportCount,
carportListDtos);
		}


		/// <summary>
		/// 通过指定id获取CarPortListDto信息
		/// </summary>
		public async Task<CarPortListDto> GetCarPortByIdAsync(EntityDto<long> input)
		{
			var entity = await _carportRepository.GetAsync(input.Id);

		    return entity.MapTo<CarPortListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCarPortForEditOutput> GetCarPortForEdit(NullableIdDto<long> input)
		{
			var output = new GetCarPortForEditOutput();
CarPortEditDto carportEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _carportRepository.GetAsync(input.Id.Value);

carportEditDto = entity.MapTo<CarPortEditDto>();

				//carportEditDto = ObjectMapper.Map<List <carportEditDto>>(entity);
			}
			else
			{
carportEditDto = new CarPortEditDto();
			}

			output.CarPort = carportEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CarPort的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateCarPort(CreateOrUpdateCarPortInput input)
		{

			if (input.CarPort.Id>0)
			{
				await UpdateCarPortAsync(input.CarPort);
			}
			else
			{
				await CreateCarPortAsync(input.CarPort);
			}
		}


		/// <summary>
		/// 新增CarPort
		/// </summary>
		//[AbpAuthorize(CarPortAppPermissions.CarPort_Create)]
		protected virtual async Task<CarPortEditDto> CreateCarPortAsync(CarPortEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CarPort>(input);

            //添加默认层
            var level = await _parkLevelsRepository.GetAll().Where(x => x.AreaId == input.AreaId).FirstOrDefaultAsync();
            entity.LevelId = level.Id;

			entity = await _carportRepository.InsertAsync(entity);
			return entity.MapTo<CarPortEditDto>();
		}

		/// <summary>
		/// 编辑CarPort
		/// </summary>
		//[AbpAuthorize(CarPortAppPermissions.CarPort_Edit)]
		protected virtual async Task UpdateCarPortAsync(CarPortEditDto input)
		{
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _carportRepository.GetAsync(input.Id);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _carportRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CarPort信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		//[AbpAuthorize(CarPortAppPermissions.CarPort_Delete)]
		public async Task DeleteCarPort(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _carportRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CarPort的方法
		/// </summary>
		          //[AbpAuthorize(CarPortAppPermissions.CarPort_BatchDelete)]
		public async Task BatchDeleteCarPortsAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _carportRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<CarTypesListDto>> GetCarTypes()
        {
            return await _carTypesAppService.GetTypesListDtos();
        }

        public async Task<List<CarPortListDto>> GetCarPortListDtosByParkId(int parkId)
        {
            var list = await _carportRepository.GetAll().Where(x => x.ParkId == parkId).ToListAsync();
            return list.Select(x => x.MapTo<CarPortListDto>()).ToList();
        }

        public async Task<List<ParkAreaDto>> GetParkAreaDtosByParkId(int parkId)
        {
            var list =await _parkAreaRepository.GetAll().Where(x => x.ParkId == parkId).ToListAsync();
            return list.Select(x => x.MapTo<ParkAreaDto>()).ToList();
        }

        public async Task<ParkCarportParkingSpaceCountDto> GetParkCarportParkingSpaceCountDto(int parkId)
        {
            var rentCount = await _carportRepository.GetAll().Where(x => x.ParkId == parkId)
                .Include(x => x.CarPortType)
                .Where(x => x.CarPortType.RentingSellingType == Enum.RentingSellingType.Rent)
                .CountAsync();
            var sellCount = await _carportRepository.GetAll().Include(x => x.CarPortType)
                .Where(x => x.CarPortType.RentingSellingType == Enum.RentingSellingType.Sell)
                .CountAsync();
            var remaingCount =await _carportRepository.GetAll().Where(x => !x.CarUserId.HasValue).CountAsync();
            return new ParkCarportParkingSpaceCountDto() {  RemainingCount=remaingCount, RentCount=rentCount, SellCount=sellCount};
        }

        /// <summary>
        /// 导出CarPort为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetCarPortsToExcel()
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


