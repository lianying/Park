
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

using Park.ParkEntrancePermissions.Authorization;
using Park.ParkEntrancePermissions.Dtos;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntrancePermissions
{
    /// <summary>
    /// ParkEntrancePermission应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(ParkEntrancePermissionAppPermissions.ParkEntrancePermission)] 
    public class ParkEntrancePermissionAppService : ParkAppServiceBase, IParkEntrancePermissionAppService
    {
    private readonly IRepository<ParkEntrancePermission, long>
    _parkentrancepermissionRepository;
    
       
       private readonly IParkEntrancePermissionManager _parkentrancepermissionManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public ParkEntrancePermissionAppService(
    IRepository<ParkEntrancePermission, long>
parkentrancepermissionRepository
        ,IParkEntrancePermissionManager parkentrancepermissionManager
        )
        {
        _parkentrancepermissionRepository = parkentrancepermissionRepository;
  _parkentrancepermissionManager=parkentrancepermissionManager;
        }


        /// <summary>
            /// 获取ParkEntrancePermission的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<ParkEntrancePermissionListDto>> GetPagedParkEntrancePermissions(GetParkEntrancePermissionsInput input)
		{

		    var query = _parkentrancepermissionRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var parkentrancepermissionCount = await query.CountAsync();

			var parkentrancepermissions = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var parkentrancepermissionListDtos = ObjectMapper.Map<List <ParkEntrancePermissionListDto>>(parkentrancepermissions);
				var parkentrancepermissionListDtos =parkentrancepermissions.MapTo<List<ParkEntrancePermissionListDto>>();

				return new PagedResultDto<ParkEntrancePermissionListDto>(
parkentrancepermissionCount,
parkentrancepermissionListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取ParkEntrancePermissionListDto信息
		/// </summary>
		public async Task<ParkEntrancePermissionListDto> GetParkEntrancePermissionByIdAsync(EntityDto<long> input)
		{
			var entity = await _parkentrancepermissionRepository.GetAsync(input.Id);

		    return entity.MapTo<ParkEntrancePermissionListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetParkEntrancePermissionForEditOutput> GetParkEntrancePermissionForEdit(NullableIdDto<long> input)
		{
			var output = new GetParkEntrancePermissionForEditOutput();
ParkEntrancePermissionEditDto parkentrancepermissionEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _parkentrancepermissionRepository.GetAsync(input.Id.Value);

parkentrancepermissionEditDto = entity.MapTo<ParkEntrancePermissionEditDto>();

				//parkentrancepermissionEditDto = ObjectMapper.Map<List <parkentrancepermissionEditDto>>(entity);
			}
			else
			{
parkentrancepermissionEditDto = new ParkEntrancePermissionEditDto();
			}

			output.ParkEntrancePermission = parkentrancepermissionEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ParkEntrancePermission的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateParkEntrancePermission(CreateOrUpdateParkEntrancePermissionInput input)
		{

			if (input.ParkEntrancePermission.Id.HasValue)
			{
				await UpdateParkEntrancePermissionAsync(input.ParkEntrancePermission);
			}
			else
			{
				await CreateParkEntrancePermissionAsync(input.ParkEntrancePermission);
			}
		}


		/// <summary>
		/// 新增ParkEntrancePermission
		/// </summary>
		[AbpAuthorize(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Create)]
		protected virtual async Task<ParkEntrancePermissionEditDto> CreateParkEntrancePermissionAsync(ParkEntrancePermissionEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <ParkEntrancePermission>(input);

			entity = await _parkentrancepermissionRepository.InsertAsync(entity);
			return entity.MapTo<ParkEntrancePermissionEditDto>();
		}

		/// <summary>
		/// 编辑ParkEntrancePermission
		/// </summary>
		[AbpAuthorize(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Edit)]
		protected virtual async Task UpdateParkEntrancePermissionAsync(ParkEntrancePermissionEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _parkentrancepermissionRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _parkentrancepermissionRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ParkEntrancePermission信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Delete)]
		public async Task DeleteParkEntrancePermission(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _parkentrancepermissionRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ParkEntrancePermission的方法
		/// </summary>
		          [AbpAuthorize(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_BatchDelete)]
		public async Task BatchDeleteParkEntrancePermissionsAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _parkentrancepermissionRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        

        /// <summary>
        /// 导出ParkEntrancePermission为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetParkEntrancePermissionsToExcel()
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


