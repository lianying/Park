using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    /// <summary>
    /// 收费标准 
    /// </summary>
    public enum CarType:byte
    {
        /// <summary>
        /// 月租车
        /// </summary>
        Month=1,
        /// <summary>
        /// 储值车   暂时禁用
        /// </summary>
        Store=2,
        /// <summary>
        /// 临时车
        /// </summary>
        Tempoary=4,
        /// <summary>
        /// 亲情车
        /// </summary>
        Family=3

    }
}
