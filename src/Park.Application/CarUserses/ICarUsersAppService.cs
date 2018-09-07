

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CarUserses.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarUserses
{
    /// <summary>
    /// CarUsers应用层服务的接口方法
    ///</summary>
    public interface ICarUsersAppService : IApplicationService
    {
        /// <summary>
    /// 获取CarUsers的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CarUsersListDto>> GetPagedCarUserss(GetCarUserssInput input);

		/// <summary>
		/// 通过指定id获取CarUsersListDto信息
		/// </summary>
		Task<CarUsersListDto> GetCarUsersByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出CarUsers为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCarUserssToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCarUsersForEditOutput> GetCarUsersForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetCarUsersForEditOutput


        /// <summary>
        /// 添加或者修改CarUsers的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCarUsers(CreateOrUpdateCarUsersInput input);


        /// <summary>
        /// 删除CarUsers信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCarUsers(EntityDto<long> input);


        /// <summary>
        /// 批量删除CarUsers
        /// </summary>
        Task BatchDeleteCarUserssAsync(List<long> input);


        //// custom codes
        Task<List<CarUsersListDto>> GetCarUsersListDtosByGroupId(int id);
		 
        //// custom codes end
    }
}
