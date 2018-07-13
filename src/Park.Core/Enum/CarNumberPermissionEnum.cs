using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Enum
{
    public enum CarNumberPermissionEnum
    {
        /// <summary>
        /// 月租车正常进入
        /// </summary>
        MonthIn,
        /// <summary>
        /// 临时车直接进入
        /// </summary>
        TempIn,
        /// <summary>
        /// 临时车确认进入
        /// </summary>
        TempConfimIn,
        /// <summary>
        /// 车位满以临时车进入
        /// </summary>
        CarportsFullIn,
        /// <summary>
        /// 无牌车禁止进入
        /// </summary>
        NoNumberConfimIn,
        /// <summary>
        /// 月租车无权限进入
        /// </summary>
        NoPermissionNotIn,
        /// <summary>
        /// 不允许临时车进入
        /// </summary>
        TempNotIn,
        /// <summary>
        /// 集团车位已满 不允许进入
        /// </summary>
        CarportsFullNotIn,
        /// <summary>
        /// 无牌车不允许进入
        /// </summary>
        NoNumberNotIn


    }
}
