using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FeeRules
{
    /// <summary>
    /// 收费者类，包含用户的实收 优惠 应收 金额
    /// </summary>
    public class Charger
    {
        public DateTime InTime { get; set; }

        public DateTime OutTime { get; set; }


        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal Pay { get; private set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal Receivable { get; private set; }

        /// <summary>
        /// 本地优惠金额
        /// </summary>
        public decimal LocalDiscountedPrice { get; set; }

        /// <summary>
        /// 本地优惠时长
        /// </summary>
        public TimeSpan? LocalDiscountedTime { get; set; }

        /// <summary>
        /// 本地优惠时长金额
        /// </summary>
        public decimal LocalDiscountedTimePrice { get; set; }



    }
}
