using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    /// <summary>
    /// 入场类型   拍照入场、手工入场
    /// </summary>
    public enum InOutTypeEnum:byte
    {
        /// <summary>
        /// 拍照进出场
        /// </summary>
        Photo,
        /// <summary>
        /// 手工进出场
        /// </summary>
        Artificial
    }
}
