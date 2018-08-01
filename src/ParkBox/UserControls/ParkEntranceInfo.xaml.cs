
using Abp.Dependency;
using Castle.Core.Logging;
using Park.Devices.Models;
using Park.Entitys.Box;
using Park.Expansions;
using Park.ParkBox;
using Park.Parks.Entrance;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Park.UserControls
{
    /// <summary>
    /// ParkEntranceInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ParkEntranceInfo : UserControl
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        private DeviceInfoDto deviceInfo;
        private readonly IParkBoxOptions _parkBoxOptions;
        private IHandler handler;
        private Image Image;
        private SynchronizationContext synchronizationContext;
        private Timer timer;
        private readonly ILogger _logger;

        IManualEntryAndExit _manualEntryAndExit = null;

        public ParkEntranceInfo(DeviceInfoDto deviceInfoDto, IParkBoxOptions parkBoxOptions, ILogger logger)
        {
            InitializeComponent();
            deviceInfo = deviceInfoDto;
            _parkBoxOptions = parkBoxOptions;
            _logger = logger;
            _manualEntryAndExit = IocManager.Instance.Resolve<IManualEntryAndExit>();
            synchronizationContext = SynchronizationContext.Current;
            if (_parkBoxOptions.IsListView)
            {

                //CoverLayerWinform coverLayer = new CoverLayerWinform(deviceInfo, parkBoxOptions, logger, async () => await OpenRod(), InOutAction);
                //coverLayer.Dock = System.Windows.Forms.DockStyle.Fill;
                //coverLayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                Grd_Left.Children.Remove(Cav_CoverLayer);
                //coverLayer.SetImage(_parkBoxOptions.DefultCarmeraImg);
                //host.Child = coverLayer;

            }
            else
            {
               // Grd_Left.Children.Remove(Cav_Winform);
                if (deviceInfo.EntranceDto.EntranceType == Enum.EntranceType.In)
                {
                    TimeClock.Foreground = new SolidColorBrush(Colors.Orange);
                    EntranceName.Foreground = new SolidColorBrush(Colors.Orange);
                }
                else
                {
                    TimeClock.Foreground = new SolidColorBrush(Colors.Green);
                    EntranceName.Foreground = new SolidColorBrush(Colors.Green);
                }
                if (deviceInfo.EntranceDto.EntranceType == Enum.EntranceType.Out)
                {
                    this.btn_InOut.Content = "手工出场";
                }
                timer = new System.Threading.Timer(x =>
                {
                    synchronizationContext.Post(z => this.TimeClock.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), null);
                }, null, 1000, 1000);

            }
        }

       

        public void Init()
        {
            if (deviceInfo == null)
            {
                _logger.Info("$init deviceInfo==null");
                return;
            }
            EntranceName.Text = deviceInfo.EntranceDto.EntranceName;
            Image = new Image();
            
            Image.SetImage(_parkBoxOptions.DefultCarmeraImg);
            if (_parkBoxOptions.IsListView)
            {
                if (deviceInfo.EquipmentManufacturers == Enum.EquipmentManufacturers.LanKa)
                {
                    var pic = new LanKaPicture();
                    handler = pic;
                    //WindowsFormsHost windowsFormsHost
                    Camera.Children.Add(pic);
                }
                else
                {
                    var pic = new HiKPicture(deviceInfo, async () => await OpenRod(), InOutAction);
                    pic.SetImage(_parkBoxOptions.DefultCarmeraImg);
                    handler = pic;
                    Camera.Children.Add(pic);
                }
                Image.Stretch = Stretch.Fill;
                Img.Children.Add(Image);
            }
            else
            {
                Img.Visibility = System.Windows.Visibility.Collapsed;
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = _parkBoxOptions.DefultCarmeraImg.GetBitmapImage();
                Camera.Background = imageBrush;

            }
            deviceInfo.Handler = handler?.IntPtr;

            //Dispatcher.Invoke(new Action(() =>
            //{
            //    this.ReSizePic();
            //}), DispatcherPriority.Loaded);
        }

        /// <summary>
        /// 入场信息
        /// </summary>
        /// <param name="carInRecord"></param>
        /// <returns></returns>
        public Task SetInfo(CarInRecord carInRecord)
        {
            var tempCarport = carInRecord.CarUser?.CarPorts?.Where(z => z.StartTime <= DateTime.Now && z.EndTime >= DateTime.Now);
            synchronizationContext.Post(x =>
            {
                txt_CarNumber.Text = carInRecord.CarNumber;
                txt_CarInCount.Text = carInRecord.CarInCount.ToString();
                txt_CarportsCount.Text = tempCarport == null ? "0" : tempCarport.Count().ToString();
                txt_InTime.Text = carInRecord.InTime.ToString("yyyy-MM-dd hh:mm:ss");
                txt_RematingDays.Text = tempCarport == null ? "0" : (tempCarport.Max(c => c.EndTime) - DateTime.Now).TotalDays.ToString();
                txt_CarType.Text = tempCarport == null ? "临时车" : tempCarport.First().CarPortType.CustomName;
                txt_UserName.Text = carInRecord.CarUser?.Name;
                txt_OutTime.Text = "";
                txt_RematingDays.Text = tempCarport == null ? "0" : (tempCarport.Max(c => c.EndTime) - DateTime.Now).TotalDays.ToString();
            }, null);


            return Task.CompletedTask;
        }

        public Task SetInfo(CarOutRecord carOutRecord)
        {

            var tempCarport = carOutRecord.CarUser?.CarPorts?.Where(z => z.StartTime <= DateTime.Now && z.EndTime >= DateTime.Now);
            synchronizationContext.Post(x => 
            {
                txt_CarNumber.Text = carOutRecord.CarNumber;
                txt_CarInCount.Text = carOutRecord.CarInCount.ToString();
                txt_CarportsCount.Text = carOutRecord == null ? "0" : tempCarport?.Count().ToString();
                txt_InTime.Text = carOutRecord.InTime.ToString("yyyy-MM-dd hh:mm:ss");
                txt_RematingDays.Text = tempCarport == null ? "0" : (tempCarport.Max(c => c.EndTime) - DateTime.Now).TotalDays.ToString();
                txt_CarType.Text = tempCarport == null ? "临时车" : tempCarport.First().CarPortType.CustomName;
                txt_UserName.Text = carOutRecord.CarUser?.Name;
                txt_OutTime.Text = carOutRecord.OutTime.ToString("yyyy-MM-dd hh:mm:ss");
            }, null);
            return Task.CompletedTask;
        }


        public Task SetImage(Stream stream=null) {
            if (stream == null)
            { //展示默认图片
               stream= _parkBoxOptions.DefultCarmeraImg;
            }
            SetImageSource(stream);
            
            return Task.CompletedTask;
        }


        private void SetImageSource(Stream stream)
        {
            Image.SetImage(stream);
        }
        /// <summary>
        /// 手工入场
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_InOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            InOutAction();
        }
        private void InOutAction()
        {

            synchronizationContext.Post(x => _manualEntryAndExit?.ManualEntryAndExit(x as EntranceDto), deviceInfo?.EntranceDto);
        }

        private async void btn_OpenRod_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenRod();
        }

        public async Task OpenRod()
        {
            if (deviceInfo == null)
            {
                _logger.Info("open rod error::::deviceInfo==null");
                return;
            }
            if (deviceInfo.Controlable != null && deviceInfo.Controlable.Camerable != null)
            {
                await deviceInfo.Controlable?.Camerable?.OpenRod();
                _logger.Info("btn openRodClick success");
            }
            else
            {
                _logger.Info("deviceInfo.Controlable ==null or deviceInfo.Controlable?.Camerable==null");
            }
            return;
        }

        public DeviceInfoDto GetDeviceInfo()
        {
            return deviceInfo;
        }

        public Tuple<double,double> GetWithAndHeight()
        {
            return new Tuple<double, double>(Grd_Left.Width, Grd_Left.Height);
        }

        

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Task.Delay(1000).ContinueWith(task =>
            //{
            //    synchronizationContext.Post((x) =>
            //    {

            //        SetWindowPos(host.Handle, new IntPtr(-1), 0, 0, 0, 0, 3);
            //    }, null);
            //});
        }
    }
}
