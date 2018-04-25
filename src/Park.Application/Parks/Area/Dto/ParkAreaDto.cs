using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.ParkAreas;
using Park.ParkBox.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Area.Dto
{
    [AutoMap(typeof(ParkAreas))]
    public class ParkAreaDto : EntityDto<long>
    {
        public ParkDto Park { get; set; }

        public string AreaName { get; set; }

        public int parkAreaCarports { get; set; }


    }
}
