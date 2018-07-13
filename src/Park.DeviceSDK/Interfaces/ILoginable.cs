using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Devices.Interfaces
{
    public interface ILoginable
    {
        /// <summary>
        /// 设备登录
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <returns>loginId</returns>
        long Login(string uName, string uPwd,long port);



        bool LoginOut(long loginId);
    }
}
