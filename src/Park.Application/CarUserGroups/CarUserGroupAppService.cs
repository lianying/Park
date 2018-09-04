
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

using Park.CarUserGroups.Authorization;
using Park.CarUserGroups.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarUserGroups
{
    /// <summary>
    /// CarUserGroup应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(CarUserGroupAppPermissions.CarUserGroup)] 
    public class CarUserGroupAppService : ParkAppServiceBase, ICarUserGroupAppService
    {
    private readonly IRepository<CarUserGroup, int>
    _carusergroupRepository;
    
       
       private readonly ICarUserGroupManager _carusergroupManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public CarUserGroupAppService(
    IRepository<CarUserGroup, int>
carusergroupRepository
        ,ICarUserGroupManager carusergroupManager
        )
        {
        _carusergroupRepository = carusergroupRepository;
  _carusergroupManager=carusergroupManager;
        }


        /// <summary>
            /// 获取CarUserGroup的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CarUserGroupListDto>> GetPagedCarUserGroups(GetCarUserGroupsInput input)
		{

		    var query = _carusergroupRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var carusergroupCount = await query.CountAsync();

			var carusergroups = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var carusergroupListDtos = ObjectMapper.Map<List <CarUserGroupListDto>>(carusergroups);
				var carusergroupListDtos =carusergroups.MapTo<List<CarUserGroupListDto>>();

				return new PagedResultDto<CarUserGroupListDto>(
carusergroupCount,
carusergroupListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取CarUserGroupListDto信息
		/// </summary>
		public async Task<CarUserGroupListDto> GetCarUserGroupByIdAsync(EntityDto<int> input)
		{
			var entity = await _carusergroupRepository.GetAsync(input.Id);

		    return entity.MapTo<CarUserGroupListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCarUserGroupForEditOutput> GetCarUserGroupForEdit(NullableIdDto<int> input)
		{
			var output = new GetCarUserGroupForEditOutput();
CarUserGroupEditDto carusergroupEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _carusergroupRepository.GetAsync(input.Id.Value);

carusergroupEditDto = entity.MapTo<CarUserGroupEditDto>();

				//carusergroupEditDto = ObjectMapper.Map<List <carusergroupEditDto>>(entity);
			}
			else
			{
carusergroupEditDto = new CarUserGroupEditDto();
			}

			output.CarUserGroup = carusergroupEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CarUserGroup的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateCarUserGroup(CreateOrUpdateCarUserGroupInput input)
		{

			if (input.CarUserGroup.Id.HasValue)
			{
				await UpdateCarUserGroupAsync(input.CarUserGroup);
			}
			else
			{
				await CreateCarUserGroupAsync(input.CarUserGroup);
			}
		}


		/// <summary>
		/// 新增CarUserGroup
		/// </summary>
		[AbpAuthorize(CarUserGroupAppPermissions.CarUserGroup_Create)]
		protected virtual async Task<CarUserGroupEditDto> CreateCarUserGroupAsync(CarUserGroupEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CarUserGroup>(input);

			entity = await _carusergroupRepository.InsertAsync(entity);
			return entity.MapTo<CarUserGroupEditDto>();
		}

		/// <summary>
		/// 编辑CarUserGroup
		/// </summary>
		[AbpAuthorize(CarUserGroupAppPermissions.CarUserGroup_Edit)]
		protected virtual async Task UpdateCarUserGroupAsync(CarUserGroupEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _carusergroupRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _carusergroupRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CarUserGroup信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(CarUserGroupAppPermissions.CarUserGroup_Delete)]
		public async Task DeleteCarUserGroup(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _carusergroupRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CarUserGroup的方法
		/// </summary>
		          [AbpAuthorize(CarUserGroupAppPermissions.CarUserGroup_BatchDelete)]
		public async Task BatchDeleteCarUserGroupsAsync(List<int> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _carusergroupRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出CarUserGroup为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetCarUserGroupsToExcel()
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


