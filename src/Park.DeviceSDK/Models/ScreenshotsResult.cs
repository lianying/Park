using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Devices.Models
{
    /// <summary>
    /// 抓图回调结果
    /// </summary>
    public class ScreenshotsResult
    {
        /// <summary>
        /// 图片
        /// </summary>
        public Stream Img { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }


        
    }
}
