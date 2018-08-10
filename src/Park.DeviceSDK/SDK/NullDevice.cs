using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Devices.Interfaces;
using Park.Devices.Models;
using Park.Models;

namespace Park.SDK
{
    public class NullDevice : ICamerable,ILedable,IControlable
    {
        
        public ICamerable Camerable { get { return this; } set { } }
        public ILedable Led { get { return this; } set { } }

       

        public long Login(string uName, string uPwd, long port)
        {
            return -1;
        }

        public bool LoginOut(long loginId)
        {
            return false;
        }

        public Task ManualCapture()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OpenRod()
        {
            return Task.FromResult<bool>(false);
        }

        public void RealTimeMonitoring(IntPtr intPtr)
        {
        }

        public ScreenshotsResult Screenshots()
        {
            return null;
        }

        public bool Show(string text)
        {
            return false;
        }

        public bool Speak(string text)
        {
            return false;
        }

        Tuple<string, Task<Stream>, ResultCarInfo, CarTypeEnum> ICamerable.GetPlateNumber()
        {
            return null;
        }
    }
}
