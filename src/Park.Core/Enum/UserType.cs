using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum UserType
    {

        [Description("业主")]
        Host,
        [Description("租户")]
        Tenant
    }
}
