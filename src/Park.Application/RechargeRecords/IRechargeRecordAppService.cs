

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.RechargeRecords.Dtos;
using Park.Entitys.CarUsers;

namespace Park.RechargeRecords
{
    /// <summary>
    /// RechargeRecord应用层服务的接口方法
    ///</summary>
    public interface IRechargeRecordAppService : IApplicationService
    {
        /// <summary>
    /// 获取RechargeRecord的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<RechargeRecordListDto>> GetPagedRechargeRecords(GetRechargeRecordsInput input);

		/// <summary>
		/// 通过指定id获取RechargeRecordListDto信息
		/// </summary>
		Task<RechargeRecordListDto> GetRechargeRecordByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出RechargeRecord为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetRechargeRecordsToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRechargeRecordForEditOutput> GetRechargeRecordForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetRechargeRecordForEditOutput


        /// <summary>
        /// 添加或者修改RechargeRecord的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateRechargeRecord(CreateOrUpdateRechargeRecordInput input);


        /// <summary>
        /// 删除RechargeRecord信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRechargeRecord(EntityDto<long> input);


        /// <summary>
        /// 批量删除RechargeRecord
        /// </summary>
        Task BatchDeleteRechargeRecordsAsync(List<long> input);


		//// custom codes
		 
        //// custom codes end
    }
}
