using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum CarDiscountEnum:Byte
    {
        /// <summary>
        /// 优惠金额
        /// </summary>
        Money=0,
        /// <summary>
        /// 优惠时长
        /// </summary>
        Time=1,
        /// <summary>
        /// 单次免费
        /// </summary>
        Free=2
    }
}
