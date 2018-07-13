using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum PayTypeEnum:byte
    {
        /// <summary>
        /// 现金
        /// </summary>
        Money,
        /// <summary>
        /// 本地优惠券
        /// </summary>
        LocalCarDisCount,
        /// <summary>
        /// 微信
        /// </summary>
        WeiXinPay,
        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay,
        /// <summary>
        /// 银行卡
        /// </summary>
        BankCard,
        /// <summary>
        /// 其他
        /// </summary>
        Other,
        /// <summary>
        /// 组合支付
        /// </summary>
        CombinationPayment

    }
}
