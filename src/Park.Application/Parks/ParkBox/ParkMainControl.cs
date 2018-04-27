using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Park.ParkBox
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkMainControl
    {
        /// <summary>
        /// 处理摄像机回调线程
        /// </summary>
        private Thread workThread;
        /// <summary>
        /// 定时调用GC
        /// </summary>
        private Thread ramThread;

        private IParkBoxOptions parkOptions;


        private void StartReleasingMemory()
        {
            ramThread = new Thread(new ThreadStart(ReleaseRemory));
            ramThread.IsBackground = true;
            ramThread.Start();  
        }

        private void ReleaseRemory()
        {
            while (true)
            {
                Utils.Utils.ReleaseMemory(false);
                Thread.Sleep(30 * 1000 * 60);
            }
        }
    }
}
