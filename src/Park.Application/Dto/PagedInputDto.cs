using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Dto
{
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public int SkipCount { get ; set; }
        [Range(1, ParkConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        public PagedInputDto()
        {
            MaxResultCount = ParkConsts.DefaultPageSize;
        }
    }
}
