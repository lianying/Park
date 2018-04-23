using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Parks
{
    [Table("Park")]
    public class JinQuPark : ParkBase, ISynchronize
    {
        public const int MaxParkSoureLength = 285;
        /// <summary>
        /// 是否同步支付宝
        /// </summary>
        public virtual bool IsSync { get; set; }
        [MaxLength(MaxParkSoureLength)]
        /// <summary>
        /// 车场来源
        /// </summary>
        public virtual string ParkSoure { get; set; }
        public virtual bool IsSuccess { get; set; }
        [MaxLength(ParkConsts.MaxCloudIdLength)]
        public virtual string CloudId { get; set; }

        [ForeignKey("ParentId")]
        public virtual JinQuPark ParentPark { get; set; }

        public virtual int? ParentId { get; set; }
    }
}
