
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

using Park.CameraInfoBases.Authorization;
using Park.CameraInfoBases.Dtos;
using Park.Entitys;

namespace Park.CameraInfoBases
{
    /// <summary>
    /// CameraInfoBase应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(CameraInfoBaseAppPermissions.CameraInfoBase)] 
    public class CameraInfoBaseAppService : ParkAppServiceBase, ICameraInfoBaseAppService
    {
    private readonly IRepository<CameraInfoBase, int>
    _camerainfobaseRepository;
    
       
       private readonly ICameraInfoBaseManager _camerainfobaseManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public CameraInfoBaseAppService(
    IRepository<CameraInfoBase, int>
camerainfobaseRepository
        ,ICameraInfoBaseManager camerainfobaseManager
        )
        {
        _camerainfobaseRepository = camerainfobaseRepository;
  _camerainfobaseManager=camerainfobaseManager;
        }


        /// <summary>
            /// 获取CameraInfoBase的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<CameraInfoBaseListDto>> GetPagedCameraInfoBases(GetCameraInfoBasesInput input)
		{

		    var query = _camerainfobaseRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var camerainfobaseCount = await query.CountAsync();

			var camerainfobases = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var camerainfobaseListDtos = ObjectMapper.Map<List <CameraInfoBaseListDto>>(camerainfobases);
				var camerainfobaseListDtos =camerainfobases.MapTo<List<CameraInfoBaseListDto>>();

				return new PagedResultDto<CameraInfoBaseListDto>(
camerainfobaseCount,
camerainfobaseListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取CameraInfoBaseListDto信息
		/// </summary>
		public async Task<CameraInfoBaseListDto> GetCameraInfoBaseByIdAsync(EntityDto<int> input)
		{
			var entity = await _camerainfobaseRepository.GetAsync(input.Id);

		    return entity.MapTo<CameraInfoBaseListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetCameraInfoBaseForEditOutput> GetCameraInfoBaseForEdit(NullableIdDto<int> input)
		{
			var output = new GetCameraInfoBaseForEditOutput();
CameraInfoBaseEditDto camerainfobaseEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _camerainfobaseRepository.GetAsync(input.Id.Value);

camerainfobaseEditDto = entity.MapTo<CameraInfoBaseEditDto>();

				//camerainfobaseEditDto = ObjectMapper.Map<List <camerainfobaseEditDto>>(entity);
			}
			else
			{
camerainfobaseEditDto = new CameraInfoBaseEditDto();
			}

			output.CameraInfoBase = camerainfobaseEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改CameraInfoBase的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateCameraInfoBase(CreateOrUpdateCameraInfoBaseInput input)
		{

			if (input.CameraInfoBase.Id.HasValue)
			{
				await UpdateCameraInfoBaseAsync(input.CameraInfoBase);
			}
			else
			{
				await CreateCameraInfoBaseAsync(input.CameraInfoBase);
			}
		}


		/// <summary>
		/// 新增CameraInfoBase
		/// </summary>
		[AbpAuthorize(CameraInfoBaseAppPermissions.CameraInfoBase_Create)]
		protected virtual async Task<CameraInfoBaseEditDto> CreateCameraInfoBaseAsync(CameraInfoBaseEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <CameraInfoBase>(input);

			entity = await _camerainfobaseRepository.InsertAsync(entity);
			return entity.MapTo<CameraInfoBaseEditDto>();
		}

		/// <summary>
		/// 编辑CameraInfoBase
		/// </summary>
		[AbpAuthorize(CameraInfoBaseAppPermissions.CameraInfoBase_Edit)]
		protected virtual async Task UpdateCameraInfoBaseAsync(CameraInfoBaseEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _camerainfobaseRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _camerainfobaseRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除CameraInfoBase信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(CameraInfoBaseAppPermissions.CameraInfoBase_Delete)]
		public async Task DeleteCameraInfoBase(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _camerainfobaseRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除CameraInfoBase的方法
		/// </summary>
		          [AbpAuthorize(CameraInfoBaseAppPermissions.CameraInfoBase_BatchDelete)]
		public async Task BatchDeleteCameraInfoBasesAsync(List<int> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _camerainfobaseRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出CameraInfoBase为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetCameraInfoBasesToExcel()
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


