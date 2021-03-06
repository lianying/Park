﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.ParkAreas
{
    public class ParkAreas : Entity<long>, ISynchronize, IAudited,IMayHaveTenant
    {
       
        public virtual Parks.ParkSet Park { get; set; }


        [ForeignKey("Park")]
        public virtual int ParkId { get; set; }
        

        public virtual string AreaName { get; set; }
        /// <summary>
        /// 区域车位数
        /// </summary>
        public virtual  int ParkAreaCarports { get; set; }

        /// <summary>
        /// 临时车车位
        /// </summary>
        public virtual  int ParkAreaTempCarports { get; set; }

        /// <summary>
        /// 固定车位
        /// </summary>
        public virtual  int ParkAreaFixedCarports { get; set; }

        public virtual bool IsSuccess { get ; set ; }
        public virtual string CloudId { get ; set ; }
        public virtual long? CreatorUserId { get ; set; }
        public virtual DateTime CreationTime { get ; set ; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual int? TenantId { get; set; }
        
        public virtual ParkAreas ParentArea { get; set; }


        [ForeignKey("ParentArea")]
        public virtual long? ParentAreaId { get; set; }
    }
}
