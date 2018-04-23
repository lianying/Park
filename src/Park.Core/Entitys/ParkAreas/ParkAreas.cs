using Abp.Domain.Entities;
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
        public virtual Parks.JinQuPark Park { get; set; }


        [ForeignKey("Park")]
        public virtual int ParkId { get; set; }
        

        public virtual string AreaName { get; set; }
        /// <summary>
        /// 区域车位数
        /// </summary>
        public virtual  int parkAreaCarports { get; set; }

        /// <summary>
        /// 可租车位
        /// </summary>
        public virtual  int parkAreaRentableCarports { get; set; }

        public virtual bool IsSuccess { get ; set ; }
        public virtual string CloudId { get ; set ; }
        public virtual long? CreatorUserId { get ; set; }
        public virtual DateTime CreationTime { get ; set ; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual int? TenantId { get; set; }
    }
}
