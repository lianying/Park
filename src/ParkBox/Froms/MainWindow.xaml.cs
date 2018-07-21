using Abp.Dependency;
using Abp.Extensions;
using Castle.MicroKernel.Registration;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.CreateCameraPnel;
using Park.Entitys.ParkEntrances;
using Park.Froms;
using Park.ParkBox;
using Park.Parks.Entrance;
using Park.Parks.ParkBox.Core;
using Park.Parks.ParkBox.Interfaces;
using Park.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Park.Froms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : AbpWindow, ISingletonDependency, IManualEntryAndExit
    {
        private readonly IParkBoxOptions parkBoxOptions;
        private readonly ICreatePnel _createPnel;
        private readonly IVehicleFlow _vehicleFlow;
        private readonly ICarNumberPermission _carNumberPermission;
        private readonly LedManager _ledManager;

        private EntranceDto _inEntranceDto = null;

        private EntranceDto _outEntranceDto = null;

        private Dictionary<long, ParkEntranceInfo> parkEntrances;

        
        
        public MainWindow(IParkBoxOptions parkBoxOptions, ICreatePnel createPnel,
            IVehicleFlow vehicleFlow,ICarNumberPermission carNumberPermission, LedManager ledManager)
        {
            InitializeComponent();
            DataContext = this;
            this.parkBoxOptions = parkBoxOptions;
            _createPnel = createPnel;
            _vehicleFlow = vehicleFlow;
            _carNumberPermission = carNumberPermission;
            _ledManager = ledManager;
            var userCard = IocManager.Instance.Resolve<UserCard>();
            UserCard.Background = new SolidColorBrush(Colors.White);
            UserCard.Child = userCard;
            

            

            IocManager.Instance.IocContainer.Register(
                Component.For<IManualEntryAndExit>().UsingFactoryMethod(() => this));

            parkEntrances = _createPnel.CreatePnels(this.ContentCamera);
            
        }

        /// <summary>
        /// 手工出入场
        /// </summary>
        /// <param name="entranceId"></param>
        public void ManualEntryAndExit(EntranceDto entranceDto)
        {
            if (entranceDto == null) return;

            var index = 0;
            if (entranceDto.EntranceType == Enum.EntranceType.Out)
            {
                _outEntranceDto = entranceDto;
                index = 1;
            }
            else {
                _inEntranceDto = entranceDto;
            }
            TiggleFlyout(index);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TiggleFlyout(0);
        }

     


        private bool TiggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return false;
            }

            flyout.IsOpen = !flyout.IsOpen;
            return flyout.IsOpen;
        }

        private async void btn_In_Click(object sender, RoutedEventArgs e)
        {
            if (_inEntranceDto == null)
            {
                await this.ShowMessageAsync("提示", "未找到出入口信息");
                return;
            }
            var carNumber = txt_InCarNumber.Text;
            if (carNumber.IsNullOrEmpty()) {
                await this.ShowMessageAsync("提示", "车牌号不允许为空!");
                return;
            }
            var result = _carNumberPermission.CheckCarNumberPermission(carNumber, _inEntranceDto.Id);
            var carInModel= new Parks.ParkBox.CarInModel()
            {
                CarNumber = carNumber,
                Img = null,
                InOutType = Enum.InOutTypeEnum.Artificial,
                InTime = DateTime.Now,
                Entrance = _inEntranceDto
            };
            var carIn = _vehicleFlow.CarIn(carInModel, result);
            if (carIn!=null)
            {
                parkEntrances[_inEntranceDto.Id]?.OpenRod();
                //await deviceInfoDto.Controlable.Camerable.OpenRod(); //抬杆
                await _ledManager.SpeakAndShowText(parkEntrances[_inEntranceDto.Id].GetDeviceInfo(), carInModel, result); //播报语音
                parkEntrances[_inEntranceDto.Id]?.SetInfo(carIn);
            }
            else
            {
                await this.ShowMessageAsync("提示", "入场失败");
                return;
            }

        }

        private async void btn_Out_Click(object sender, RoutedEventArgs e)
        {
            if (_outEntranceDto == null)
            {
                await this.ShowMessageAsync("提示", "未找到出入口信息");
                return;
            }
            var carNumber = txt_OutCarNumber.Text;
            if (carNumber.IsNullOrEmpty())
            {
                await this.ShowMessageAsync("提示", "车牌号不允许为空!");
                return;
            }
            var isCarIn = _vehicleFlow.IsCarIn(_outEntranceDto.ParkLevel.Park.Id, carNumber);
            if (isCarIn.IsCarIn)
            {
                var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now });
                if (outRcode!=null)
                {

                    parkEntrances[_outEntranceDto.Id]?.SetInfo(outRcode);
                }
                else {

                    await this.ShowMessageAsync("提示", "出场失败!");
                }
            }
            else {

                await this.ShowMessageAsync("提示", "当前车辆不在场内!");
            }
        }
    }
}
