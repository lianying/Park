using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum EntranceType
    {
        [Description("入口")]
        In,
        [Description("出口")]
        Out
    }
}
