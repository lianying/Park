

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CarUserGroups.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarUserGroups
{
    /// <summary>
    /// CarUserGroup应用层服务的接口方法
    ///</summary>
    public interface ICarUserGroupAppService : IApplicationService
    {
        /// <summary>
    /// 获取CarUserGroup的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CarUserGroupListDto>> GetPagedCarUserGroups(GetCarUserGroupsInput input);

		/// <summary>
		/// 通过指定id获取CarUserGroupListDto信息
		/// </summary>
		Task<CarUserGroupListDto> GetCarUserGroupByIdAsync(EntityDto<int> input);


        /// <summary>
        /// 导出CarUserGroup为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCarUserGroupsToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCarUserGroupForEditOutput> GetCarUserGroupForEdit(NullableIdDto<int> input);

        //todo:缺少Dto的生成GetCarUserGroupForEditOutput


        /// <summary>
        /// 添加或者修改CarUserGroup的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCarUserGroup(CreateOrUpdateCarUserGroupInput input);


        /// <summary>
        /// 删除CarUserGroup信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCarUserGroup(EntityDto<int> input);


        /// <summary>
        /// 批量删除CarUserGroup
        /// </summary>
        Task BatchDeleteCarUserGroupsAsync(List<int> input);


        //// custom codes
        Task<List<CarUserGroupListDto>> GetCarUserGroupListDtosByAreaId(long AreaId);


        Task<List<CarUserGroupListDto>> GetCarUserGroupListDtos(int parkId);
        //// custom codes end
    }
}
