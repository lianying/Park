
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Park.Devices.Interfaces;
using Park.Entitys;
using Park.Expansions;
using Park.Devices.Models;
using Park.Models;
using AxBLC_IPNCLib;
using System.Windows.Forms;

namespace Park.DeviceSDK.LanKa
{
    /// <summary>
    /// DeviceSDK中日志记录使用EventLog
    /// </summary>
	public class LanKaControl : ICamerable, IControlable,ILedable {

        public readonly static string[] keys = {
                "BCA_TEXT_REMAIN_SPACE",
                "BCA_TEXT_CHARGE",
                "BCA_TEXT_PLATE",
                "BCA_TEXT_FIXED_VALID_DATE",
                "BCA_TEXT_CAR_RESTRICTIONS",
                "BCA_TEXT_CAR_TYPE",
                "BCA_TEXT_CAR_OWER",
                "BCA_TEXT_CAR_DURATION",
                "BCA_TEXT_CHANNEL_NAME",
                "BCA_TEXT_CAR_IN_TIME",
                "BCA_TEXT_CAR_OUT_TIME",
                "BCA_TEXT_CONTENT0",
                "BCA_TEXT_CONTENT1",
                "BCA_TEXT_CONTENTN",
                "BCA_TEXT_WEATHER",
                "BCA_IMG_CAR_IN",
                "BCA_IMG_CAR_OUT",
                "BCA_IMG_AD0",
                "BCA_IMG_AD1",
                "BCA_IMG_ADN",
                "BCA_IMG_QRCODE"
         };
		
		/// <summary>
		/// 蓝卡设备不需要登录  
		/// 登录名保存的为 接口程序的URL地址
		/// 密码为 
		/// </summary>
		CameraInfoBase LanKaDeviceCamare;
		private Action<StringBuilder> DeiveLogHandler;

		private Action<CameraInfoBase> SDKCalBackHandler;  //触发自动拍照的回调函数   

		private Action<CameraInfoBase> ChangeStatusHandler;

		Func<string, CameraInfoBase> GetDeviceInfoByDeviceID = null;

		Action<IntPtr,string> LanKaRealPlayCalBack = null;

		private Action<int> OpenSDKCalBack; //抬杆回调   用于更新显示屏信息
		//private MyHttpServer Listen;
		Action<IntPtr> StopPlay;

        private System.Threading.Timer _time;
		public LanKaControl(Action<CameraInfoBase> SDKCalBackHandler, Action<CameraInfoBase> ChangeStatusHandler, Action<StringBuilder> DeiveLogHandler, CameraInfoBase LanKaDeviceCamare, Action<int> OpenSDKCalBack = null, Func<string, CameraInfoBase> GetDeviceInfoByDeviceID = null, Action<IntPtr, string> LanKaRealPlayCalBack = null,Action<IntPtr> StopPlay=null) {
			this.LanKaDeviceCamare = LanKaDeviceCamare;
			this.SDKCalBackHandler = SDKCalBackHandler;
			this.ChangeStatusHandler = ChangeStatusHandler;
			this.DeiveLogHandler = DeiveLogHandler;
			this.OpenSDKCalBack = OpenSDKCalBack;
			this.GetDeviceInfoByDeviceID = GetDeviceInfoByDeviceID;
			this.LanKaRealPlayCalBack = LanKaRealPlayCalBack;
			this.StopPlay = StopPlay;
		}

        public LanKaControl(SDKControlParametes controlParametes)
        {
            this.LanKaDeviceCamare = controlParametes?.CameraInfoBase;
            this.SDKCalBackHandler = controlParametes?.SDKCalBackHandler;
            this.LanKaRealPlayCalBack = (x, y) =>
            {
                try
                {
                    var ax = Form.FromHandle(x) as AxBLC_IPNC;
                    if (ax != null)
                    {
                        ax.PlayS("rtsp://" + y + ":8557/H.264");
                    }
                }
                catch (Exception e)
                {
                    WriteLog.EventLog("PlayCalBack error " + e.Message);
                }
            };
            this.GetDeviceInfoByDeviceID = controlParametes.GetDeviceInfoByDeviceID;
        }

		public Task ManualCapture() {
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("DeviceID", LanKaDeviceCamare.Port);
			dic.Add("DeviceIP", LanKaDeviceCamare.Ip);
			string a = JsonUtility.ObjectToJson(dic);
			HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "Picture", a);

            return Task.CompletedTask;
		}



        public void MockTest()
        {
            if (!MyHttpServer.isEventBind)
            {
                Task.Delay(1000).ContinueWith((task) => _time = new System.Threading.Timer((x) => { _time_Elapsed(null, null); }, null, 0, 100 * 60));
            }
            
        }   

        private void _time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random r = new Random();
            string result = "{\"Detail\":\"0000005162,"+ParkConsts.DeviceIds[r.Next(0, ParkConsts.DeviceIds.Length)] +"_20180806121108_0000005161_1R.jpg,,1,128,"+ParkConsts.CarNumberList[r.Next(0, ParkConsts.CarNumberList.Length)] +",蓝牌,,0.00,0:0:0,513,582,734,687,-0.196700,100,,0,0,0;\u0000h\"}";
            this.MyHttpServer_EventNewrecord(result);
        }

        private  Stream GetPic(int count)
        {
            if (this.LanKaDeviceCamare.Image != null)
            {
                return LanKaDeviceCamare.Image;
            }
            CameraInfoBase dev = this.LanKaDeviceCamare; //calBackParames as CameraInfoBase;
            Stream stream = new MemoryStream();
            LankaResultInfo info = dev.CalBackData as LankaResultInfo;
            if (count < 10)
            {
                string imgPath = "Image/" + DateTime.Now.ToString("yyyyMMdd") + "/" + info.DeviceId + "/" + info.ImgName;
                if (File.Exists(imgPath))
                {
                    FileStream fs = new FileStream(imgPath, FileMode.Open);
                    fs.Position = 0;
                    //fs.Seek(0, SeekOrigin.Begin);
                    fs.CopyTo(stream);
                    fs.Close();
                    WriteLog.EventLog(imgPath);
                }
                else
                {
                    //JinQuLogs.LogHelper.Info("Not Exists " + imgPath);
                    Task.Delay(10);
                    count++;
                    return  GetPic(count);
                }
            }
            LanKaDeviceCamare.Image = stream;
            return stream;
        }

		public Task<bool> OpenRod() {
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("DeviceID", this.LanKaDeviceCamare.Port);
			dic.Add("DeviceIP", this.LanKaDeviceCamare.Ip);
			string a = JsonUtility.ObjectToJson(dic);
			HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "OpenGate", a);
            //if (OpenSDKCalBack != null) OpenSDKCalBack(this.LanKaDeviceCamare.ControlId);
            return Task.FromResult<bool>(true);
		}
        void MyHttpServer_EventNewrecord(string result)
        {

            WriteLog.EventLog(result);
            //result = "{\"Detail\":\"0000002686,1801490988_20180226140931_0000002687_1B.jpg,,1,90,鲁APX188,蓝牌,鲁APX188,0.00,0:0:0,1390,388,1505,428,-0.089500,100,,0,0,0;1801490988_20180226140931_0000002687_1B.jpg,鲁APX188,鲁APX188\u0000?\"}";
            var Detail = JsonUtility.JsonToObject(result, typeof(LanKaResultModel)) as LanKaResultModel;
            var model = new LankaResultInfo(Detail.Detail);


            var device = GetDeviceInfoByDeviceID(model.DeviceId);
            if (device != null)
            {
                if (device.Image != null)
                {
                    device.Image.Dispose();
                    device.Image = null;
                }
                device.CalBackData = model;
                SDKCalBackHandler(device);
            }
            else
            {
                this.LanKaDeviceCamare.CalBackData = model;
                SDKCalBackHandler(this.LanKaDeviceCamare);
            }
            //GetPic(0);

        }

		

        public ICamerable Camerable { get { return this; } set { } }
        public ILedable Led { get { return this; } set { } }

        

		

		public bool ShowText(string carNumber, int remting, string text,int entranceType) {
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("Page", "in_temp_car.html");
			dic.Add("Key", GetKeys(carNumber, remting, text, string.Empty, null));
			dic.Add("DeviceID", this.LanKaDeviceCamare.Port);
			dic.Add("DeviceIP", this.LanKaDeviceCamare.Ip);
			string a = JsonUtility.ObjectToJson(dic);
			var result = HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "NewLCD", a);
			return true;
		}

        public bool Speak(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Voice", text);
            dic.Add("DeviceID", this.LanKaDeviceCamare.Port);
            dic.Add("DeviceIP", this.LanKaDeviceCamare.Ip);
            string a = JsonUtility.ObjectToJson(dic);

            var result = HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "PlayVoice", a);
            return true;
        }

		public bool ShowQR(string carNumber, int remting, string text, string url,int entranceType, decimal? money) {

			//string Page = "in_temp_car.html"; //in_temp_car.html    out_temp_car.html   out_fixed_pass.html
			Dictionary<string, object> dic = new Dictionary<string, object>();
			//if (carNumber.IsNullOrEmpty() || carNumber.IsNoNumber()) {
			//	if (text.IsNullOrEmpty()) {
			//		Page = "out_idle.html";
			//	}
			//	else
			//		Page = "out_fixed_pass.html";
			//}
			//else if (money.HasValue) {
			//	Page = "out_temp_car.html";
			//}
			bool isMothOut;
			string Page = getPageName(carNumber, remting, text, url, entranceType, money, out isMothOut);
			dic.Add("Page", Page);
			dic.Add("Key", GetKeys(carNumber, remting, text, url, money, isMothOut));
			dic.Add("DeviceID", this.LanKaDeviceCamare.Port);
			dic.Add("DeviceIP", this.LanKaDeviceCamare.Ip);
			string a = JsonUtility.ObjectToJson(dic);
			var result = HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.Ip + "NewLCD", a);
			return true;
		}
		private string getPageName(string carNumber, int remting, string text, string url,int entranceType, decimal? money,out bool isMothOut)
		{
			string page="in_temp_car.html";
			isMothOut = false;
			if(carNumber.IsNullOrEmpty()||carNumber.IsNoNumber()){
				if(text.IsNullOrEmpty()){
					if(entranceType==1){
						page = "in_idle.html";
					}else{
						page = "out_idle.html";
					}
				}
				else 
					page = "out_fixed_pass.html";
			}
			else if(money.HasValue){
				page = "out_temp_car.html";
			}
			else if (text.Contains("此卡可用")) {
				page = "out_fixed_pass.html";
				isMothOut = true;
			}


			return page;
		}
		public bool ShowQR(string carNumber, int remting, string text, Stream stream,int entranceType, decimal? money) {
			return false;
		}

		public static object GetKeys(string carNumber,int remting,string text,string url, decimal? money,bool isMothOut=false) {
			string value = string.Empty;
			List<KeyItem> list = new List<KeyItem>();
			foreach (var key in keys) {

				if (string.IsNullOrEmpty(key) || key.ToLower() == "voice") continue;

				if (key.ToUpper() == "BCA_TEXT_PLATE") {
					if (isMothOut)
						value = text;
					else
						value = carNumber.Trim();
				}
				else if (key.ToUpper() == "BCA_TEXT_REMAIN_SPACE") {
					value = remting < 0 ? 0.ToString() : remting.ToString();
				}
				else if (key.ToUpper() == "BCA_TEXT_CONTENT0") {
					value = text.Trim();
				}
				else if (key.ToUpper() == "BCA_IMG_QRCODE") {
					value = url;
				}
				else if (money.HasValue && key.ToUpper() == "BCA_TEXT_CHARGE") {
					value = money.Value.ToString("#0.00");
				}
				list.Add(new KeyItem(key, 0, value));
				value = string.Empty;
			}
			return list;
		}

        public long Login(string uName, string uPwd, long port)
        {
            if (!MyHttpServer.isEventBind)
            {
                MyHttpServer.EventNewrecord += MyHttpServer_EventNewrecord;
                MyHttpServer.isEventBind = true;
            }

            MyHttpServer.StartListen(this.LanKaDeviceCamare.Password);
            var getDevice = HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "GetDevList", "");
            //if (!string.IsNullOrEmpty(getDevice) && getDevice.Contains(this.LanKaDeviceCamare.Ip))
            //{
                LanKaDeviceCamare.DeviceStatus = Enum.DeviceStatus.Online; //更改登录状态
                this.LanKaDeviceCamare.LoginId = port;
                if (DeiveLogHandler != null) DeiveLogHandler(new StringBuilder(LanKaDeviceCamare.Ip + " 登录成功"));
                if (ChangeStatusHandler != null) ChangeStatusHandler(LanKaDeviceCamare);
           // }
            //else
            //{
                if (DeiveLogHandler != null) DeiveLogHandler(new StringBuilder(LanKaDeviceCamare.Ip + " 登录失败"));
           // }
            return LanKaDeviceCamare.LoginId;
        }

        public bool LoginOut(long loginId)
        {
            if (StopPlay != null) StopPlay(this.LanKaDeviceCamare.Handler.Value);
            return true;
        }

        public void RealTimeMonitoring(IntPtr intPtr)
        {
            this.LanKaDeviceCamare.Handler = intPtr;
            LanKaRealPlayCalBack(intPtr, LanKaDeviceCamare.Ip);
            //return 1;
        }

        public ScreenshotsResult Screenshots()
        {
            return null;
        }

        

        public Tuple<string, Task<Stream>, ResultCarInfo, CarTypeEnum> GetPlateNumber()
        {
            LankaResultInfo info = this.LanKaDeviceCamare.CalBackData as LankaResultInfo;
            var task = Task.Run(() => GetPic(0));
            return new Tuple<string, Task<Stream>, ResultCarInfo, CarTypeEnum>(info.CarNumber, task, null, CarTypeEnum.MotorVehicle);
        }

        public bool Show(string text)
        {
            var result = HttpUtility.SendHttpRequest(this.LanKaDeviceCamare.UserName + "NewLCD", text);
            return true;
        }
    }
	


	class KeyItem {
		public KeyItem(string strName, int type, string strData) {
			this.Name = strName;
			this.Type = type;
			this.Data = strData;
		}
		public string Name { get; set; }
		public int Type { get; set; }
		public string Data { get; set; }
	}
}
