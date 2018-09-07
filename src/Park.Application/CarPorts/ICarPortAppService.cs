

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CarPorts.Dtos;
using Park.CarTypeses.Dtos;
using Park.Entitys.CarTypes;
using Park.Entitys.CarUsers;
using Park.ParkAreases.Dtos;

namespace Park.CarPorts
{
    /// <summary>
    /// CarPort应用层服务的接口方法
    ///</summary>
    public interface ICarPortAppService : IApplicationService
    {
        /// <summary>
    /// 获取CarPort的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CarPortListDto>> GetPagedCarPorts(GetCarPortsInput input);

		/// <summary>
		/// 通过指定id获取CarPortListDto信息
		/// </summary>
		Task<CarPortListDto> GetCarPortByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出CarPort为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCarPortsToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCarPortForEditOutput> GetCarPortForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetCarPortForEditOutput


        /// <summary>
        /// 添加或者修改CarPort的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCarPort(CreateOrUpdateCarPortInput input);


        /// <summary>
        /// 删除CarPort信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCarPort(EntityDto<long> input);


        /// <summary>
        /// 批量删除CarPort
        /// </summary>
        Task BatchDeleteCarPortsAsync(List<long> input);


        //// custom codes

        Task<List<CarTypesListDto>> GetCarTypes();

        Task<List<CarPortListDto>> GetCarPortListDtosByParkId(int parkId);


        Task<List<ParkAreaDto>> GetParkAreaDtosByParkId(int parkId);


        Task<ParkCarportParkingSpaceCountDto> GetParkCarportParkingSpaceCountDto(int parkId);


        Task<List<CarPortListDto>> GetCarPortListDtosByUserId(long userId);
		 
        //// custom codes end
    }
}
