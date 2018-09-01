

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.ParkEntranceses.Dtos;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntranceses
{
    /// <summary>
    /// ParkEntrances应用层服务的接口方法
    ///</summary>
    public interface IParkEntrancesAppService : IApplicationService
    {
        /// <summary>
    /// 获取ParkEntrances的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ParkEntrancesListDto>> GetPagedParkEntrancess(GetParkEntrancessInput input);

		/// <summary>
		/// 通过指定id获取ParkEntrancesListDto信息
		/// </summary>
		Task<ParkEntrancesListDto> GetParkEntrancesByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出ParkEntrances为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetParkEntrancessToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetParkEntrancesForEditOutput> GetParkEntrancesForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetParkEntrancesForEditOutput


        /// <summary>
        /// 添加或者修改ParkEntrances的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateParkEntrances(CreateOrUpdateParkEntrancesInput input);


        /// <summary>
        /// 删除ParkEntrances信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteParkEntrances(EntityDto<long> input);


        /// <summary>
        /// 批量删除ParkEntrances
        /// </summary>
        Task BatchDeleteParkEntrancessAsync(List<long> input);


        //// custom codes
        Task<List<ParkEntrancesListDto>> GetParkEntrancesListDtos(int parkId);



        Task<List<ParkEntrancesListDto>> GetParkEntrancesListDtosByAreaId(long areaId);

        //// custom codes end
    }
}
