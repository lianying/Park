using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Interfaces;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.Parks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park.Entitys.ParkLevels
{
    public class ParkLevel:Entity,ISynchronize, IAudited
    {
        public const int MaxLevelNameLength = 285;
        /// <summary>
        /// 层名称
        /// </summary>
        [Required]
        [MaxLength(MaxLevelNameLength)]
        public virtual string LevelName { get; set; }
        /// <summary>
        /// 层号
        /// </summary>
        [Required]
        [Range(-10,30)]
        public virtual int LevelNumber { get; set; }


        public virtual bool IsSuccess { get; set; }

        [MaxLength(ParkConsts.MaxCloudIdLength)]
        public virtual string CloudId { get; set; }
        public virtual long? CreatorUserId { get ; set ; }
        public virtual DateTime CreationTime { get ; set ; }
        public virtual long? LastModifierUserId { get ; set ; }
        public virtual DateTime? LastModificationTime { get; set ; }
        [ForeignKey("ParkId")]
        public virtual JinQuPark Park { get; set; }

        public virtual int ParkId { get; set; }
    }
}
