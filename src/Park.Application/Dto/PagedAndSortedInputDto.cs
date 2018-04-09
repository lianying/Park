﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Dto
{
    public class PagedAndSortedInputDto: PagedInputDto,ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInputDto()
        {
            MaxResultCount = ParkConsts.DefaultPageSize;
        }
    }
}
