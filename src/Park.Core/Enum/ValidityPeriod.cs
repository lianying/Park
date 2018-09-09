using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum ValidityPeriod
    {
        All,
        /// <summary>
        /// 有效
        /// </summary>
        Effective,
        /// <summary>
        /// 临期
        /// </summary>
        Advance,
        /// <summary>
        /// 过期
        /// </summary>
        Expired
    }
}
