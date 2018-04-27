using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Devices.Interfaces
{
    public interface ICamareable : IControlable
    {
        /// <summary>
		/// 手动抓拍
		/// </summary>
		void NUAL_SNAP(DeviceInfoDto devics);

        /// <summary>
        /// 启动实时监视
        /// </summary>
        /// <param name="device"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        int RealPlay(DeviceInfoDto device, IntPtr handler);
        
    }
}
