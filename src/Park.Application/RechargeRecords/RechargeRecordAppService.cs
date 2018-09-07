
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

using Park.RechargeRecords.Authorization;
using Park.RechargeRecords.Dtos;
using Park.Entitys.CarUsers;

namespace Park.RechargeRecords
{
    /// <summary>
    /// RechargeRecord应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(RechargeRecordAppPermissions.RechargeRecord)] 
    public class RechargeRecordAppService : ParkAppServiceBase, IRechargeRecordAppService
    {
    private readonly IRepository<RechargeRecord, long>
    _rechargerecordRepository;
    
       
       private readonly IRechargeRecordManager _rechargerecordManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public RechargeRecordAppService(
    IRepository<RechargeRecord, long>
rechargerecordRepository
        ,IRechargeRecordManager rechargerecordManager
        )
        {
        _rechargerecordRepository = rechargerecordRepository;
  _rechargerecordManager=rechargerecordManager;
        }


        /// <summary>
            /// 获取RechargeRecord的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<RechargeRecordListDto>> GetPagedRechargeRecords(GetRechargeRecordsInput input)
		{

		    var query = _rechargerecordRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var rechargerecordCount = await query.CountAsync();

			var rechargerecords = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var rechargerecordListDtos = ObjectMapper.Map<List <RechargeRecordListDto>>(rechargerecords);
				var rechargerecordListDtos =rechargerecords.MapTo<List<RechargeRecordListDto>>();

				return new PagedResultDto<RechargeRecordListDto>(
rechargerecordCount,
rechargerecordListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取RechargeRecordListDto信息
		/// </summary>
		public async Task<RechargeRecordListDto> GetRechargeRecordByIdAsync(EntityDto<long> input)
		{
			var entity = await _rechargerecordRepository.GetAsync(input.Id);

		    return entity.MapTo<RechargeRecordListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetRechargeRecordForEditOutput> GetRechargeRecordForEdit(NullableIdDto<long> input)
		{
			var output = new GetRechargeRecordForEditOutput();
RechargeRecordEditDto rechargerecordEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _rechargerecordRepository.GetAsync(input.Id.Value);

rechargerecordEditDto = entity.MapTo<RechargeRecordEditDto>();

				//rechargerecordEditDto = ObjectMapper.Map<List <rechargerecordEditDto>>(entity);
			}
			else
			{
rechargerecordEditDto = new RechargeRecordEditDto();
			}

			output.RechargeRecord = rechargerecordEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改RechargeRecord的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateRechargeRecord(CreateOrUpdateRechargeRecordInput input)
		{

			if (input.RechargeRecord.Id.HasValue)
			{
				await UpdateRechargeRecordAsync(input.RechargeRecord);
			}
			else
			{
				await CreateRechargeRecordAsync(input.RechargeRecord);
			}
		}


		/// <summary>
		/// 新增RechargeRecord
		/// </summary>
		[AbpAuthorize(RechargeRecordAppPermissions.RechargeRecord_Create)]
		protected virtual async Task<RechargeRecordEditDto> CreateRechargeRecordAsync(RechargeRecordEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <RechargeRecord>(input);

			entity = await _rechargerecordRepository.InsertAsync(entity);
			return entity.MapTo<RechargeRecordEditDto>();
		}

		/// <summary>
		/// 编辑RechargeRecord
		/// </summary>
		[AbpAuthorize(RechargeRecordAppPermissions.RechargeRecord_Edit)]
		protected virtual async Task UpdateRechargeRecordAsync(RechargeRecordEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _rechargerecordRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _rechargerecordRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除RechargeRecord信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(RechargeRecordAppPermissions.RechargeRecord_Delete)]
		public async Task DeleteRechargeRecord(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _rechargerecordRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除RechargeRecord的方法
		/// </summary>
		          [AbpAuthorize(RechargeRecordAppPermissions.RechargeRecord_BatchDelete)]
		public async Task BatchDeleteRechargeRecordsAsync(List<long> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _rechargerecordRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出RechargeRecord为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetRechargeRecordsToExcel()
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


