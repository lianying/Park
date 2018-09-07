

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.MonthFees.Dtos;
using Park.Entitys.FareRules;

namespace Park.MonthFees
{
    /// <summary>
    /// MonthFee应用层服务的接口方法
    ///</summary>
    public interface IMonthFeeAppService : IApplicationService
    {
        /// <summary>
    /// 获取MonthFee的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MonthFeeListDto>> GetPagedMonthFees(GetMonthFeesInput input);

		/// <summary>
		/// 通过指定id获取MonthFeeListDto信息
		/// </summary>
		Task<MonthFeeListDto> GetMonthFeeByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出MonthFee为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetMonthFeesToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetMonthFeeForEditOutput> GetMonthFeeForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetMonthFeeForEditOutput


        /// <summary>
        /// 添加或者修改MonthFee的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateMonthFee(CreateOrUpdateMonthFeeInput input);


        /// <summary>
        /// 删除MonthFee信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteMonthFee(EntityDto<int> input);


        /// <summary>
        /// 批量删除MonthFee
        /// </summary>
        Task BatchDeleteMonthFeesAsync(List<int> input);


		//// custom codes
		 
        //// custom codes end
    }
}
