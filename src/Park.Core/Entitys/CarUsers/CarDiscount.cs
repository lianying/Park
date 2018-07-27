using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.CarUsers
{
    /// <summary>
    /// 平台下发优惠券
    /// </summary>
    public class CarDiscount : Entity<long>, IHasCreationTime, ISoftDelete
    {
        public virtual string DiscountId { get; set; }

        public virtual CarDiscountEnum DiscountType { get; set; }

        [StringLength(20)]
        public virtual string CarNumber { get; set; }

        [StringLength(200)]
        public virtual string DiscountValue { get; set; }


        public virtual DateTime DiscountExpire { get; set; }


        public virtual string CloudParkId { get; set; }





        public virtual DateTime CreationTime { get ; set ; }
        public virtual bool IsDeleted { get; set; }


        public virtual int ParkId { get; set; }

    }
}
