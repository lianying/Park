

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.ParkAreases.Dtos;
using Park.Entitys.ParkAreas;

namespace Park.ParkAreases
{
    /// <summary>
    /// ParkAreas应用层服务的接口方法
    ///</summary>
    public interface IParkAreasAppService : IApplicationService
    {
        /// <summary>
    /// 获取ParkAreas的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ParkAreasListDto>> GetPagedParkAreass(GetParkAreassInput input);

		/// <summary>
		/// 通过指定id获取ParkAreasListDto信息
		/// </summary>
		Task<ParkAreasListDto> GetParkAreasByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出ParkAreas为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetParkAreassToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetParkAreasForEditOutput> GetParkAreasForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetParkAreasForEditOutput


        /// <summary>
        /// 添加或者修改ParkAreas的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateParkAreas(CreateOrUpdateParkAreasInput input);


        /// <summary>
        /// 删除ParkAreas信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteParkAreas(EntityDto<long> input);


        /// <summary>
        /// 批量删除ParkAreas
        /// </summary>
        Task BatchDeleteParkAreassAsync(List<long> input);


        //// custom codes
        /// <summary>
        /// TreeMenu
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        Task<List<ParkAreaDto>> GetParkAreaDtosGroupByParent(int parkId);

        /// <summary>
        /// 获取所有父级区域
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        Task<List<ParkAreaDto>> GetParkAreaAllParents(int parkId);


        Task<List<ParkAreaDto>> GetParkAreaDtos(int parkId);
        //// custom codes end
       
    }
}
