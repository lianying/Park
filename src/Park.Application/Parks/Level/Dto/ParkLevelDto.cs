using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.ParkLevels;
using Park.ParkBox.Dto;
using Park.Parks.Area.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Level.Dto
{
    [AutoMap(typeof(ParkLevels))]
    public class ParkLevelDto : EntityDto<long>
    {
        public ParkDto Park { get; set; }

        public string LevelName { get; set; }

        public int LevelNumber { get; set; }
        
    }
}
