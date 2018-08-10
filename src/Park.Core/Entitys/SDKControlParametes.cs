using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys
{
    public class SDKControlParametes
    {

        public CameraInfoBase CameraInfoBase { get; set; }

        public Action<CameraInfoBase> SDKCalBackHandler { get; set; }  //触发自动拍照的回调函数 


        public  Func<string, CameraInfoBase> GetDeviceInfoByDeviceID { get; set; }



       // public Action<IntPtr, string> LanKaRealPlayCalBack { get; set; }
    }
}
