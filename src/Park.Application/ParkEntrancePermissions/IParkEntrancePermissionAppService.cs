

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.ParkEntrancePermissions.Dtos;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntrancePermissions
{
    /// <summary>
    /// ParkEntrancePermission应用层服务的接口方法
    ///</summary>
    public interface IParkEntrancePermissionAppService : IApplicationService
    {
        /// <summary>
    /// 获取ParkEntrancePermission的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ParkEntrancePermissionListDto>> GetPagedParkEntrancePermissions(GetParkEntrancePermissionsInput input);

		/// <summary>
		/// 通过指定id获取ParkEntrancePermissionListDto信息
		/// </summary>
		Task<ParkEntrancePermissionListDto> GetParkEntrancePermissionByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出ParkEntrancePermission为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetParkEntrancePermissionsToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetParkEntrancePermissionForEditOutput> GetParkEntrancePermissionForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetParkEntrancePermissionForEditOutput


        /// <summary>
        /// 添加或者修改ParkEntrancePermission的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateParkEntrancePermission(CreateOrUpdateParkEntrancePermissionInput input);


        /// <summary>
        /// 删除ParkEntrancePermission信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteParkEntrancePermission(EntityDto<long> input);


        /// <summary>
        /// 批量删除ParkEntrancePermission
        /// </summary>
        Task BatchDeleteParkEntrancePermissionsAsync(List<long> input);


        //// custom codes
        //// custom codes end
    }
}
