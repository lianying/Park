using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    public enum SDKCarType
    {
        /// <summary>
        /// 机动车
        /// </summary>
        Vehicles,
        /// <summary>
        /// 非机动车
        /// </summary>
        NonVehicles
    }
    /// <summary>
    /// 车牌类型
    /// </summary>
    public enum PlateType
    {
        [Description("标准民用车与军车车牌")]
        /// <summary>
        /// 标准民用车与军车车牌
        /// </summary>
        VCA_STANDARD92_PLATE = 0,
        [Description("02式民用车牌 ")]
        /// <summary>
        /// 02式民用车牌 
        /// </summary>
        VCA_STANDARD02_PLATE,
        [Description("武警车车牌")]
        /// <summary>
        /// 武警车车牌 
        /// </summary>
        VCA_WJPOLICE_PLATE,
        [Description("警车车牌")]
        /// <summary>
        /// 警车车牌 
        /// </summary>
        VCA_JINGCHE_PLATE,
        [Description("民用车双行尾牌")]
        /// <summary>
        /// 民用车双行尾牌 
        /// </summary>
        STANDARD92_BACK_PLATE,
        [Description("使馆车牌")]
        /// <summary>
        /// 使馆车牌 
        /// </summary>
        VCA_SHIGUAN_PLATE,
        [Description("农用车车牌")]
        /// <summary>
        /// 农用车车牌 
        /// </summary>
        VCA_NONGYONG_PLATE,
        [Description("摩托车车牌")]
        /// <summary>
        /// 摩托车车牌  
        /// </summary>
        VCA_MOTO_PLATE
    }

    /// <summary>
    /// 车牌颜色
    /// </summary>
    public enum PlateColor
    {
        [Description("蓝色车牌")]
        /// <summary>
        /// 蓝色车牌 
        /// </summary>
        VCA_BLUE_PLATE = 0,
        [Description("黄色车牌")]
        /// <summary>
        /// 黄色车牌
        /// </summary>
        VCA_YELLOW_PLATE,
        [Description("白色车牌")]
        /// <summary>
        /// 白色车牌 
        /// </summary>
        VCA_WHITE_PLATE,
        [Description("黑色车牌")]
        /// <summary>
        /// 黑色车牌 
        /// </summary>
        VCA_BLACK_PLATE,
        [Description("绿色车牌")]
        /// <summary>
        /// 绿色车牌 
        /// </summary>
        VCA_GREEN_PLATE,
        [Description("民航黑色车牌")]
        /// <summary>
        /// 民航黑色车牌 
        /// </summary>
        VCA_BKAIR_PLATE,
        [Description("其他")]
        /// <summary>
        /// 其他
        /// </summary>
        VCA_OTHER = 0xff
    }

    /// <summary>
    /// 车身颜色
    /// </summary>
    public enum CarColor
    {
        其他色 = 0,
        白色 = 1,
        银色 = 2,
        灰色 = 3,
        黑色 = 4,
        红色 = 5,
        深蓝 = 6,
        蓝色 = 7,
        黄色 = 8,
        绿色 = 9,
        棕色 = 10,
        粉色 = 11,
        紫色 = 12,
        深灰 = 13,
        青色 = 14,
        未进行车身颜色识别 = 0xff
    }

}
