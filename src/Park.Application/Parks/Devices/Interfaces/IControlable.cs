using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Devices.Interfaces
{
    /// <summary>
    /// 控制设备方法
    /// </summary>
    public interface IControlable
    {

        bool Login(IDeviceable deviceInfo);


        bool LoginOut(IDeviceable deviceInfo);
    }
}
