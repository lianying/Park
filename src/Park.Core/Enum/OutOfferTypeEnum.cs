using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
   public  enum OutOfferTypeEnum
    {
        /// <summary>
        /// 正常通行
        /// </summary>
        NormalPass,
        /// <summary>
        /// 免费通行
        /// </summary>
        FreePass,
        /// <summary>
        /// 优惠券支付
        /// </summary>
        CouponPass,
        /// <summary>
        /// 其他原因
        /// </summary>
        Other
    }
}
