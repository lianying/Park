using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum CarPortType
    {
        [Description("标准车位")]
        标准车位 = 1,
        [Description("字母车位")]
        字母车位 = 2,
        [Description("微型车位")]
        微型车位 = 3,
        [Description("无障碍车位")]
        无障碍车位 = 4,
        [Description("女士车位")]
        女士车位 = 5,
        [Description("机械车位")]
        机械车位 = 6,
        [Description("卸货车位")]
        卸货车位 = 7,
        [Description("路内车位")]
        路内车位 = 8,
        [Description("超长车位")]
        超长车位 = 9,
        [Description("其他")]
        其他 = 10
    }
}
