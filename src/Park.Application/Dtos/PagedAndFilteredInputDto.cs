

using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace  Park.Dtos
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        [Range(1, ParkConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string Filter { get; set; }


		//// custom codes

        //// custom codes end


        public PagedAndFilteredInputDto()
        {
            MaxResultCount = ParkConsts.DefaultPageSize;
        }
    }
}