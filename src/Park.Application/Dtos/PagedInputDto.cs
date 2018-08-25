

using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Park.Dtos
{
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(1, ParkConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }



		//// custom codes

        //// custom codes end


        public PagedInputDto()
        {
            MaxResultCount = ParkConsts.DefaultPageSize;
        }
    }
}