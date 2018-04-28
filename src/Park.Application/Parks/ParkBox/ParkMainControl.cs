using Park.Common;
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
        /// <summary>
        /// 进出场详情日志
        /// </summary>
        public ActionQueue<string> carInOutDetsilsLogQueue;
        
        

        public ParkMainControl(IParkBoxOptions parkBoxOptions)
        {
            parkOptions = parkBoxOptions;
            InitAllQueue();
        }


        private void InitAllQueue()
        {
            carInOutDetsilsLogQueue = new ActionQueue<string>();
        }


        private void StartReleasingMemory()
        {
            ramThread = new Thread(new ThreadStart(ReleaseRemory));
            ramThread.IsBackground = true;
            ramThread.Start();  
        }

        private void CleanAllQueue()
        {

            carInOutDetsilsLogQueue?.Clear();
        }

        private void ReleaseRemory()
        {
            while (true)
            {
                Utils.Utils.ReleaseMemory(false);
                CleanAllQueue();
                Thread.Sleep(30 * 1000 * 60);
            }
        }
    }
}
