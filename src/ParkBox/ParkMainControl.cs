using Abp.Dependency;
using Abp.UI;
using Park.Common;
using Park.Devices.Models;
using Park.Entitys;
using Park.Froms;
using Park.ParkBox;
using Park.Parks.ParkBox.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Park
{
    public class ParkMainControl : ITransientDependency
    {

        private bool isStop = false;
        /// <summary>
        /// 处理摄像机回调线程
        /// </summary>
        private Thread workThread;
        /// <summary>
        /// 定时调用GC
        /// </summary>
        private Thread ramThread;


        /// <summary>
        /// 配置信息
        /// </summary>
        private IParkBoxOptions parkOptions;


        private IBoxMessage boxMessageable;


        /// <summary>
        /// 进出场详情日志
        /// </summary>
        private ActionQueue<string> carInOutDetsilsLogQueue;

        /// <summary>
        /// 存放摄像机消息队列
        /// </summary>
        private ConcurrentQueue<CameraInfoBase> messageQueue;


        private Action<CameraInfoBase> SDKCallBackAction = null;


        private Func<string,CameraInfoBase> GetDeviceByDeviceId;







        public ParkMainControl(IParkBoxOptions parkBoxOptions, IBoxMessage boxMessageable)
        {
            if (parkBoxOptions.DeciceInfos.Count == 0)
                throw new UserFriendlyException("岗亭未绑定设备");
            parkOptions = parkBoxOptions;
            GetDeviceByDeviceId = (x) =>
            {
                return parkBoxOptions.DeciceInfos.FirstOrDefault(y => y.Port == Convert.ToInt64(x));
            };

            SDKCallBackAction = (x) =>
            {
                this.messageQueue.Enqueue(x);
            };
            this.boxMessageable = boxMessageable;
            InitAllQueue();
            StartReleasingMemory();
            StartThreads();
        }
        [STAThread]
        /// <summary>
        /// 启动线程
        /// </summary>
        private void StartThreads()
        {
            CameraInfoBase cameraInfoBase;
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            workThread = new Thread(async () =>
            {
                while (true)
                {
                    if (messageQueue.Count > 0)
                    {
                        if (messageQueue.TryDequeue(out cameraInfoBase)) {
                            var t = new Thread(() => boxMessageable.DoMessage(cameraInfoBase as DeviceInfoDto));
                            t.SetApartmentState(ApartmentState.STA);
                            t.IsBackground = true;
                            t.Start();
                            t.Join();

                            //Task.Delay(0).ContinueWith((task) => boxMessageable.DoMessage(cameraInfoBase as DeviceInfoDto), scheduler);
                        }
                    }
                    await Task.Delay(10);
                }
            });
            workThread.SetApartmentState(ApartmentState.STA);
            workThread.Start();
            

            //登录所有设备
            DeviceLogin();

            ///重连在登录后5秒执行
            Task.Delay(5000).ContinueWith((task) =>
            {
                Thread reconnection = new Thread(new ThreadStart(ReconnectionThread));
                reconnection.Start();
            });


        }
        /// <summary>
        /// 设备重连线程
        /// </summary>
        private async void ReconnectionThread()
        {
            while (!isStop)
            {
                var devices = parkOptions.DeciceInfos.Where(x => x.DeviceStatus == Enum.DeviceStatus.Offline);
                if (devices != null && devices.Count() > 0)
                {
                    foreach (var device in devices)
                    {
                        DeviceLogin(device);
                       
                    }
                }

                await Task.Delay(parkOptions.ReLoginTime);
            }
        }

        private void DeviceLogin()
        {
            foreach (var device in parkOptions.DeciceInfos)
            {

                device.InitDevice(new SDKControlParametes()
                {
                    CameraInfoBase = device,
                    SDKCalBackHandler = this.SDKCallBackAction,
                    GetDeviceInfoByDeviceID = this.GetDeviceByDeviceId
                });
                DeviceLogin(device);
            }
        }


        private void InitAllQueue()
        {
            carInOutDetsilsLogQueue = new ActionQueue<string>();
            messageQueue = new ConcurrentQueue<CameraInfoBase>();
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


        private bool DeviceLogin(DeviceInfoDto deviceInfoDto)
        {
            long loginId = -1;

            loginId = deviceInfoDto.Controlable.Login(deviceInfoDto.UserName, deviceInfoDto.Password, deviceInfoDto.Port);

            if (deviceInfoDto.DeviceStatus == Enum.DeviceStatus.Online && deviceInfoDto.Handler.HasValue)
            {
                deviceInfoDto.Controlable.Camerable.RealTimeMonitoring(deviceInfoDto.Handler.Value);
            }

            return loginId > 0;
        }


    }
}
