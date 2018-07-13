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
        public string DiscountId { get; set; }

        public CarDiscountEnum DiscountType { get; set; }

        [StringLength(20)]
        public string CarNumber { get; set; }

        [StringLength(200)]
        public string DiscountValue { get; set; }


        public DateTime DiscountExpire { get; set; }


        public string CloudParkId { get; set; }





        public DateTime CreationTime { get ; set ; }
        public bool IsDeleted { get; set; }
    }
}
