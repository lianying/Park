
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

using Park.MonthFees.Authorization;
using Park.MonthFees.Dtos;
using Park.Entitys.FareRules;

namespace Park.MonthFees
{
    /// <summary>
    /// MonthFee应用层服务的接口实现方法  
    ///</summary>
[AbpAuthorize(MonthFeeAppPermissions.MonthFee)] 
    public class MonthFeeAppService : ParkAppServiceBase, IMonthFeeAppService
    {
    private readonly IRepository<MonthFee,int >
    _monthfeeRepository;
    
       
       private readonly IMonthFeeManager _monthfeeManager;

    /// <summary>
        /// 构造函数 
        ///</summary>
    public MonthFeeAppService(
    IRepository<MonthFee, int>
monthfeeRepository
        ,IMonthFeeManager monthfeeManager
        )
        {
        _monthfeeRepository = monthfeeRepository;
  _monthfeeManager=monthfeeManager;
        }


        /// <summary>
            /// 获取MonthFee的分页列表信息
            ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  async  Task<PagedResultDto<MonthFeeListDto>> GetPagedMonthFees(GetMonthFeesInput input)
		{

		    var query = _monthfeeRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件

			var monthfeeCount = await query.CountAsync();

			var monthfees = await query
					.OrderBy((x)=>input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

				// var monthfeeListDtos = ObjectMapper.Map<List <MonthFeeListDto>>(monthfees);
				var monthfeeListDtos =monthfees.MapTo<List<MonthFeeListDto>>();

				return new PagedResultDto<MonthFeeListDto>(
monthfeeCount,
monthfeeListDtos
					);
		}


		/// <summary>
		/// 通过指定id获取MonthFeeListDto信息
		/// </summary>
		public async Task<MonthFeeListDto> GetMonthFeeByIdAsync(EntityDto<int> input)
		{
			var entity = await _monthfeeRepository.GetAsync(input.Id);

		    return entity.MapTo<MonthFeeListDto>();
		}

		/// <summary>
		/// MPA版本才会用到的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async  Task<GetMonthFeeForEditOutput> GetMonthFeeForEdit(NullableIdDto<int> input)
		{
			var output = new GetMonthFeeForEditOutput();
MonthFeeEditDto monthfeeEditDto;

			if (input.Id.HasValue)
			{
				var entity = await _monthfeeRepository.GetAsync(input.Id.Value);

monthfeeEditDto = entity.MapTo<MonthFeeEditDto>();

				//monthfeeEditDto = ObjectMapper.Map<List <monthfeeEditDto>>(entity);
			}
			else
			{
monthfeeEditDto = new MonthFeeEditDto();
			}

			output.MonthFee = monthfeeEditDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改MonthFee的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public async Task CreateOrUpdateMonthFee(CreateOrUpdateMonthFeeInput input)
		{

			if (input.MonthFee.Id.HasValue)
			{
				await UpdateMonthFeeAsync(input.MonthFee);
			}
			else
			{
				await CreateMonthFeeAsync(input.MonthFee);
			}
		}


		/// <summary>
		/// 新增MonthFee
		/// </summary>
		[AbpAuthorize(MonthFeeAppPermissions.MonthFee_Create)]
		protected virtual async Task<MonthFeeEditDto> CreateMonthFeeAsync(MonthFeeEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

			var entity = ObjectMapper.Map <MonthFee>(input);

			entity = await _monthfeeRepository.InsertAsync(entity);
			return entity.MapTo<MonthFeeEditDto>();
		}

		/// <summary>
		/// 编辑MonthFee
		/// </summary>
		[AbpAuthorize(MonthFeeAppPermissions.MonthFee_Edit)]
		protected virtual async Task UpdateMonthFeeAsync(MonthFeeEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _monthfeeRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _monthfeeRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除MonthFee信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[AbpAuthorize(MonthFeeAppPermissions.MonthFee_Delete)]
		public async Task DeleteMonthFee(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _monthfeeRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除MonthFee的方法
		/// </summary>
		          [AbpAuthorize(MonthFeeAppPermissions.MonthFee_BatchDelete)]
		public async Task BatchDeleteMonthFeesAsync(List<int> input)
		{
			//TODO:批量删除前的逻辑判断，是否允许删除
			await _monthfeeRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出MonthFee为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetMonthFeesToExcel()
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


