

using Abp.Application.Services.Dto;

namespace Park.Dtos
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }


		//// custom codes

        //// custom codes end

        public PagedAndSortedInputDto()
        {
            MaxResultCount = ParkConsts.DefaultPageSize;
        }
    }
}