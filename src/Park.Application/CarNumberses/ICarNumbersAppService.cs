

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CarNumberses.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarNumberses
{
    /// <summary>
    /// CarNumbers应用层服务的接口方法
    ///</summary>
    public interface ICarNumbersAppService : IApplicationService
    {
        /// <summary>
    /// 获取CarNumbers的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CarNumbersListDto>> GetPagedCarNumberss(GetCarNumberssInput input);

		/// <summary>
		/// 通过指定id获取CarNumbersListDto信息
		/// </summary>
		Task<CarNumbersListDto> GetCarNumbersByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出CarNumbers为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCarNumberssToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCarNumbersForEditOutput> GetCarNumbersForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetCarNumbersForEditOutput


        /// <summary>
        /// 添加或者修改CarNumbers的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCarNumbers(CreateOrUpdateCarNumbersInput input);


        /// <summary>
        /// 删除CarNumbers信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCarNumbers(EntityDto<long> input);


        /// <summary>
        /// 批量删除CarNumbers
        /// </summary>
        Task BatchDeleteCarNumberssAsync(List<long> input);


        //// custom codes
        Task<List<CarNumbersListDto>> GetCarNumbersListDtosByUserId(long userId);
		 
        //// custom codes end
    }
}
