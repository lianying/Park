using Park.Devices.Models;
using Park.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Devices.Interfaces
{
    /// <summary>
    /// 摄像机接口
    /// </summary>
    public interface ICamerable:ILoginable
    {
        /// <summary>
        /// 实时监控
        /// </summary>
        /// <param name="intPtr"></param>
        void RealTimeMonitoring(IntPtr intPtr);

        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        ScreenshotsResult Screenshots();

        /// <summary>
        /// 手动抓拍
        /// </summary>
        /// <param name="loginId"></param>
        Task ManualCapture();

        /// <summary>
        /// 抬杆
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        Task<bool> OpenRod();

        Tuple<string,Stream, ResultCarInfo,CarTypeEnum> GetPlateNumber();

    }
}
