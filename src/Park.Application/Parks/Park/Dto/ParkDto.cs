using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Entitys.Parks;
using Abp.Application.Services.Dto;

namespace Park.ParkBox.Dto
{
    [AutoMap(typeof(JinQuPark))]
    public  class ParkDto:EntityDto
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public int CarportCount { get; set; }

        public string Address { get; set; }
    }
}
