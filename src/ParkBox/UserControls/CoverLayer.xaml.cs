using Castle.Core.Logging;
using Park.Devices.Models;
using Park.ParkBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.UserControls
{
    /// <summary>
    /// CoverLayer.xaml 的交互逻辑
    /// </summary>
    public partial class CoverLayer : UserControl
    {
        private DeviceInfoDto deviceInfo;
        private readonly IParkBoxOptions _parkBoxOptions;
        //private IHandler handler;
        //private Image Image;
        private SynchronizationContext synchronizationContext;
        private Timer timer;
        private readonly ILogger _logger;
        private Action OpenRod;
        private Action InOutAction;
        public CoverLayer(DeviceInfoDto deviceInfoDto, IParkBoxOptions parkBoxOptions, ILogger logger, Action openRod, Action inOutAction)
        {
            InitializeComponent();
            _parkBoxOptions = parkBoxOptions;
            _logger = logger;
            deviceInfo = deviceInfoDto;
            synchronizationContext = SynchronizationContext.Current;
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
            timer = new Timer(x =>
            {
                synchronizationContext.Post(z => this.TimeClock.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), null);
            }, null, 1000, 1000);
            OpenRod = openRod;
            InOutAction = inOutAction;
        }

        private void btn_InOut_Click(object sender, RoutedEventArgs e)
        {
            InOutAction?.Invoke();
        }

        private void btn_OpenRod_Click(object sender, RoutedEventArgs e)
        {
            OpenRod?.Invoke();
        }

        
    }
}
