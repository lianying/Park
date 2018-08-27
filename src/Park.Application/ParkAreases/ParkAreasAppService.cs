
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;

using Park.ParkAreases.Authorization;
using Park.ParkAreases.Dtos;
using Park.Entitys.ParkAreas;
using System.Data.Entity;

namespace Park.ParkAreases
{
    /// <summary>
    /// ParkAreas应用层服务的接口实现方法  
    ///</summary>
    //[AbpAuthorize(ParkAreasAppPermissions.ParkAreas)]
    public class ParkAreasAppService : ParkAppServiceBase, IParkAreasAppService
    {
        private readonly IRepository<ParkAreas, long>
        _parkareasRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public ParkAreasAppService(
        IRepository<ParkAreas, long>
    parkareasRepository
            )
        {
            _parkareasRepository = parkareasRepository;
        }


        /// <summary>
        /// 获取ParkAreas的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ParkAreasListDto>> GetPagedParkAreass(GetParkAreassInput input)
        {

            var query = _parkareasRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件

            var parkareasCount = await query.CountAsync();

            var parkareass = await query
                    .OrderBy(x => input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var parkareasListDtos = ObjectMapper.Map<List <ParkAreasListDto>>(parkareass);
            var parkareasListDtos = parkareass.MapTo<List<ParkAreasListDto>>();

            return new PagedResultDto<ParkAreasListDto>(
parkareasCount,
parkareasListDtos
                );
        }


        /// <summary>
        /// 通过指定id获取ParkAreasListDto信息
        /// </summary>
        public async Task<ParkAreasListDto> GetParkAreasByIdAsync(EntityDto<long> input)
        {
            var entity = await _parkareasRepository.GetAsync(input.Id);

            return entity.MapTo<ParkAreasListDto>();
        }

        /// <summary>
        /// MPA版本才会用到的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetParkAreasForEditOutput> GetParkAreasForEdit(NullableIdDto<long> input)
        {
            var output = new GetParkAreasForEditOutput();
            ParkAreasEditDto parkareasEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _parkareasRepository.GetAsync(input.Id.Value);

                parkareasEditDto = entity.MapTo<ParkAreasEditDto>();

                //parkareasEditDto = ObjectMapper.Map<List <parkareasEditDto>>(entity);
            }
            else
            {
                parkareasEditDto = new ParkAreasEditDto();
            }

            output.ParkAreas = parkareasEditDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改ParkAreas的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateParkAreas(CreateOrUpdateParkAreasInput input)
        {

            if (input.ParkAreas.Id.HasValue)
            {
                await UpdateParkAreasAsync(input.ParkAreas);
            }
            else
            {
                await CreateParkAreasAsync(input.ParkAreas);
            }
        }


        /// <summary>
        /// 新增ParkAreas
        /// </summary>
        //[AbpAuthorize(ParkAreasAppPermissions.ParkAreas_Create)]
        protected virtual async Task<ParkAreasEditDto> CreateParkAreasAsync(ParkAreasEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<ParkAreas>(input);

            entity = await _parkareasRepository.InsertAsync(entity);
            return entity.MapTo<ParkAreasEditDto>();
        }

        /// <summary>
        /// 编辑ParkAreas
        /// </summary>
        //[AbpAuthorize(ParkAreasAppPermissions.ParkAreas_Edit)]
        protected virtual async Task UpdateParkAreasAsync(ParkAreasEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _parkareasRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _parkareasRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除ParkAreas信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[AbpAuthorize(ParkAreasAppPermissions.ParkAreas_Delete)]
        public async Task DeleteParkAreas(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _parkareasRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除ParkAreas的方法
        /// </summary>
        //[AbpAuthorize(ParkAreasAppPermissions.ParkAreas_BatchDelete)]
        public async Task BatchDeleteParkAreassAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _parkareasRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出ParkAreas为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetParkAreassToExcel()
        //{
        //    var users = await UserManager.Users.ToListAsync();
        //    var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //    await FillRoleNames(userListDtos);
        //    return _userListExcelExporter.ExportToFile(userListDtos);
        //}



        //// custom codes
        public async Task<List<ParkAreaDto>> GetParkAreaDtos(int parkId)
        {
            var parent = await _parkareasRepository.GetAll().Where(x => !x.ParentAreaId.HasValue&&x.ParkId== parkId).ToListAsync();
            var result = parent.Select(x => x.MapTo<ParkAreaDto>()).ToList();
            long id = 0;
            for (int i = 0; i < result.Count; i++)
            {
                id = result[i].Id;
                var list = _parkareasRepository.GetAll().Where(x =>x.ParentAreaId.HasValue&& x.ParentAreaId.Value == id).ToList();
                var temp =list.Select(x => x.MapTo<ParkAreaDto>());
                result[i].ParkAreas = new System.Collections.ObjectModel.ObservableCollection<ParkAreaDto>(temp);
            }
            return result;
        }

        public async Task<List<ParkAreaDto>> GetParkAreaAllParents(int parkId)
        {

            var parent = await _parkareasRepository.GetAll().Where(x => !x.ParentAreaId.HasValue && x.ParkId == parkId).ToListAsync();
            return parent.Select(x => x.MapTo<ParkAreaDto>()).ToList();
        }

        //// custom codes end

    }
}


