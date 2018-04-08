using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Parks
{
    public class ParkBase : FullAuditedEntity<int>, IMayHaveTenant, IAudited
    {

        public const int MaxParkNameLength = 285;
        public const int MaxAreaCodeLength = 16;
        
        /// <summary>
        /// 车场名
        /// </summary>
        [Required]
        [MaxLength(MaxParkNameLength)]
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
        [Range(0,int.MaxValue)]
        public virtual int CarportCount { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        /// 
        [Required]
        public virtual decimal Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        /// 
        [Required]
        public virtual decimal Latitude { get; set; }

        [Required]
        [MaxLength(MaxAreaCodeLength)]
        /// <summary>
        /// 区域编码
        /// </summary>
        public virtual string AreaCode { get; set; }
        

        
        public virtual  int? TenantId { get; set; }
    }
}
