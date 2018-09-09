

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CameraInfoBases.Dtos;
using Park.Entitys;

namespace Park.CameraInfoBases
{
    /// <summary>
    /// CameraInfoBase应用层服务的接口方法
    ///</summary>
    public interface ICameraInfoBaseAppService : IApplicationService
    {
        /// <summary>
    /// 获取CameraInfoBase的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CameraInfoBaseListDto>> GetPagedCameraInfoBases(GetCameraInfoBasesInput input);

		/// <summary>
		/// 通过指定id获取CameraInfoBaseListDto信息
		/// </summary>
		Task<CameraInfoBaseListDto> GetCameraInfoBaseByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出CameraInfoBase为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCameraInfoBasesToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCameraInfoBaseForEditOutput> GetCameraInfoBaseForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetCameraInfoBaseForEditOutput


        /// <summary>
        /// 添加或者修改CameraInfoBase的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCameraInfoBase(CreateOrUpdateCameraInfoBaseInput input);


        /// <summary>
        /// 删除CameraInfoBase信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCameraInfoBase(EntityDto<int> input);


        /// <summary>
        /// 批量删除CameraInfoBase
        /// </summary>
        Task BatchDeleteCameraInfoBasesAsync(List<int> input);


		//// custom codes
		 
        //// custom codes end
    }
}
