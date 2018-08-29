

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.CarTypeses.Dtos;
using Park.Entitys.CarTypes;

namespace Park.CarTypeses
{
    /// <summary>
    /// CarTypes应用层服务的接口方法
    ///</summary>
    public interface ICarTypesAppService : IApplicationService
    {
        /// <summary>
    /// 获取CarTypes的分页列表信息
    ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CarTypesListDto>> GetPagedCarTypess(GetCarTypessInput input);

		/// <summary>
		/// 通过指定id获取CarTypesListDto信息
		/// </summary>
		Task<CarTypesListDto> GetCarTypesByIdAsync(EntityDto<long> input);


        /// <summary>
        /// 导出CarTypes为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetCarTypessToExcel();

        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCarTypesForEditOutput> GetCarTypesForEdit(NullableIdDto<long> input);

        //todo:缺少Dto的生成GetCarTypesForEditOutput


        /// <summary>
        /// 添加或者修改CarTypes的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateCarTypes(CreateOrUpdateCarTypesInput input);


        /// <summary>
        /// 删除CarTypes信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCarTypes(EntityDto<long> input);


        /// <summary>
        /// 批量删除CarTypes
        /// </summary>
        Task BatchDeleteCarTypessAsync(List<long> input);


        Task<List<CarTypesListDto>> GetTypesListDtos();


		//// custom codes
		 
        //// custom codes end
    }
}
