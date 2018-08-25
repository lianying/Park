using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.Parks;
using Park.ParkBox.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Park.Dto
{
    [AutoMap(typeof(ParkSet),typeof(ParkDto))]
    public class CreateParkDto:EntityDto<int>
    {
        /// <summary>
        /// 车场名
        /// </summary>
        [Required]
        [MaxLength(ParkBase.MaxParkNameLength)]
        public virtual string Name { get; set; }

        [Required]
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 车位数
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public virtual int CarportCount { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [Required]
        public virtual decimal Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        /// 
        [Required]
        public virtual decimal Latitude { get; set; }

        [Required]
        [MaxLength(ParkBase.MaxAreaCodeLength)]
        /// <summary>
        /// 区域编码
        /// </summary>
        public virtual string AreaCode { get; set; }
        public virtual bool IsSync { get; set; }
        [MaxLength(ParkSet.MaxParkSoureLength)]
        /// <summary>
        /// 车场来源
        /// </summary>
        public virtual string ParkSoure { get; set; }



        public virtual string PropertyParty { get; set; }

        /// <summary>
        /// 运营方
        /// </summary>
        public virtual string Operator { get; set; }
    }
}
