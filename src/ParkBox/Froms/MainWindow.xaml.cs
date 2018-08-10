using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Castle.MicroKernel.Registration;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.CreateCameraPnel;
using Park.Entitys.Box;
using Park.Entitys.CarTypes;
using Park.Entitys.CarUsers;
using Park.Entitys.FareRules;
using Park.Entitys.ParkEntrances;
using Park.Enum;
using Park.Froms;
using Park.ParkBox;
using Park.Parks.Entrance;
using Park.Parks.ParkBox;
using Park.Parks.ParkBox.Core;
using Park.Parks.ParkBox.Interfaces;
using Park.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        private readonly IRepository<CarTypes, long> _repositoryCarType;
        private readonly IRepository<RangeTime> _repositoryRangeTime;
        private readonly IRepository<BlackList,int > _repositoryBlackList;
        private readonly IRepository<CarPort, long> _repositoryCarPort;
        private readonly ParkMainControl _parkMainControl;


        private readonly IRepository<FareRule> _repositoryFareRule;

        private EntranceDto _inEntranceDto = null;

        private EntranceDto _outEntranceDto = null;

        private Dictionary<long, ISetInfo> parkEntrances;

        
        
        public MainWindow(IParkBoxOptions parkBoxOptions, ICreatePnel createPnel,
            IVehicleFlow vehicleFlow,
            ICarNumberPermission carNumberPermission,
            LedManager ledManager,
            IRepository<CarTypes,long> repositoryCarType,
           IRepository<FareRule> repositoryFareRule,
           IRepository<RangeTime> repositoryRangeTime,
           IRepository<BlackList, int> repositoryBlackList,
           IRepository<CarPort, long> repositoryCarPort,
           ParkMainControl parkMainControl)
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

            _repositoryCarType = repositoryCarType;
            _repositoryFareRule = repositoryFareRule;
            _repositoryRangeTime = repositoryRangeTime;
            _repositoryBlackList = repositoryBlackList;
            _repositoryCarPort = repositoryCarPort;
            _parkMainControl = parkMainControl;

            IocManager.Instance.IocContainer.Register(
                Component.For<IManualEntryAndExit>().UsingFactoryMethod(() => this));
            //IocManager.Instance.IocContainer.Register(Component.For<TaskScheduler>().Instance(TaskScheduler.FromCurrentSynchronizationContext()).LifestyleSingleton());
            IocManager.Instance.IocContainer.Register(Component.For<SynchronizationContext>().Instance(base.SynchronizationContext).LifestyleSingleton());


            Title = parkBoxOptions.ParkName;
            parkEntrances = _createPnel.CreatePnels(this.ContentCamera);
            parkBoxOptions.SetInfosDic = parkEntrances;
#if Release
     this.TopMost=true;
#endif

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
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                var backList = await _repositoryBlackList.FirstOrDefaultAsync(x => x.CarNumber == carNumber);
                if (backList != null)
                {
                    if (!parkBoxOptions.IsListView)
                    {
                        var cancle = await this.ShowMessageAsync("提示", "当前车辆在黑名单是否放行？", MessageDialogStyle.AffirmativeAndNegative);
                        if (cancle == MessageDialogResult.Negative)
                            return;
                    }
                    else
                    { //打开实时监控画面时   Metro自带的弹窗会被盖住 采用系统弹窗
                        var cancle = MessageBox.Show( "当前车辆在黑名单是否放行？", "提示", MessageBoxButton.OKCancel);
                        if (cancle == MessageBoxResult.Cancel)
                            return;
                    }
                }

                var result = _carNumberPermission.CheckCarNumberPermission(carNumber, _inEntranceDto.Id);
                var carInModel = new Parks.ParkBox.CarInModel()
                {
                    CarNumber = carNumber,
                    Img = null,
                    InOutType = Enum.InOutTypeEnum.Artificial,
                    InTime = DateTime.Now,
                    Entrance = _inEntranceDto
                };
                if(result.IsCarIn.HasValue&&!result.IsCarIn.Value)
                {
                    Logger.Debug(carNumber + "no permission" + result.ToString());
                    if (!parkBoxOptions.IsListView)
                    {
                        var cancle = await this.ShowMessageAsync("当前车辆无权进入是否放行？", "提示", MessageDialogStyle.AffirmativeAndNegative);
                        if (cancle == MessageDialogResult.Negative)
                            return;
                    }
                    else
                    {
                        var cancle = MessageBox.Show("提示", "当前车辆无权进入是否放行？", MessageBoxButton.OKCancel);
                        if (cancle ==  MessageBoxResult.Cancel)
                            return;
                    }
                }
                //入场时检查是否有场内记录 
                var isCarIn = _vehicleFlow.IsCarIn(_inEntranceDto.ParkLevel.Park.Id, carNumber);
                if (isCarIn.IsCarIn)
                {
                    var carOutModel = new CarOutModel()
                    {
                        CarInRecord = isCarIn.CarInRecord,
                        InOutType = Enum.InOutTypeEnum.Artificial,
                        OutTime = DateTime.Now,
                        Receivable = 0,
                        ParkId = _inEntranceDto.ParkLevel.Park.Id,
                        Remark = "有场内纪录，再次入场"

                    };
                    _vehicleFlow.CarErrorOut(isCarIn.CarInRecord, carOutModel);
                }
                var carIn = _vehicleFlow.CarIn(carInModel, result);
                if (carIn != null)
                {
                    await parkEntrances[_inEntranceDto.Id]?.OpenRod();
                    //await deviceInfoDto.Controlable.Camerable.OpenRod(); //抬杆
                    await _ledManager.SpeakAndShowText((parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carInModel, result); //播报语音
                    parkEntrances[_inEntranceDto.Id]?.SetInfo(carIn);
                }
                else
                {
                    await this.ShowMessageAsync("提示", "入场失败");
                    return;
                }
                await unitOfWork.CompleteAsync();
            }

        }
        private async void btn_Out_Click(object sender, RoutedEventArgs e)
        {
            #region   移到接口实现  接口实现中无法调用收费界面
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
            using (var unitOfWork= UnitOfWorkManager.Begin())
            {
                var isCarIn = _vehicleFlow.IsCarIn(_outEntranceDto.ParkLevel.Park.Id, carNumber);
                if (isCarIn.IsCarIn)
                {

                    var user = _carNumberPermission.GetUser(_outEntranceDto.ParkLevel.Park.Id, carNumber);
                    if (user != null)
                    {
                        var typeId = user.CarPorts.FirstOrDefault()?.CarPortTypeId;
                        var carType = _repositoryCarType.GetAll().FirstOrDefault(x => x.Id == (typeId.HasValue ? typeId.Value : parkBoxOptions.TempCarTypeId));
                        //月租车正常出场
                        if (carType.Category == CarType.Month)
                        {
                            if (!isCarIn.CarInRecord.IsMonthTempIn)
                            {

                                var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now });
                                if (outRcode != null)
                                {
                                    parkEntrances[_outEntranceDto.Id]?.SetInfo( outRcode);
                                }
                                else
                                {

                                    await this.ShowMessageAsync("提示", "出场失败!");
                                }
                            }
                            else
                            {  //月租车收费
                                DateTime outTime = DateTime.Now;

                                FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);
                                var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                                fareRule.TimeRangeList = rangTimes;

                                if (isCarIn.CarInRecord.TempConvertMonthTime.HasValue)
                                {
                                    outTime = isCarIn.CarInRecord.TempConvertMonthTime.Value;
                                }
                                var receivable = fareRule.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0);
                                var carOutModel = new CarOutModel()
                                {
                                    CarInRecord = isCarIn.CarInRecord,
                                    InOutType = Enum.InOutTypeEnum.Artificial,
                                    OutTime = DateTime.Now,
                                    Receivable = receivable,
                                    ParkId = _outEntranceDto.ParkLevel.Park.Id
                                };
                                if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                                {
                                    var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                                    if (outRcode != null)
                                    {
                                        await parkEntrances[_outEntranceDto.Id]?.SetInfo(outRcode);

                                        await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                                        await _ledManager.SpeakAndShowText((parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                    }
                                    else
                                    {

                                        await this.ShowMessageAsync("提示", "出场失败!");
                                    }
                                    return;
                                }


                                await _ledManager.SpeakAndShowText((parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.CalculationFee); //播报语音
                                var tollWindow = new ChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, (parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(),_repositoryFareRule,_repositoryRangeTime,null);
                                tollWindow.Init();
                                var isFree = tollWindow.ShowDialog();
                                if (isFree.HasValue && isFree.Value)
                                {


                                    await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                                    await parkEntrances[_outEntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);
                                    await _ledManager.SpeakAndShowText((parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                }
                            }
                        }
                        else
                        {
                            //其他车类型
                            DateTime outTime = DateTime.Now;

                            FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == carType.Id);
                            var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                            fareRule.TimeRangeList = rangTimes;

                            var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;
                            var carOutModel = new CarOutModel()
                            {
                                CarInRecord = isCarIn.CarInRecord,
                                InOutType = Enum.InOutTypeEnum.Artificial,
                                OutTime = outTime,
                                Receivable = receivable,
                                ParkId = _outEntranceDto.ParkLevel.Park.Id
                            };

                            if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                            {
                                var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                                if (outRcode != null)
                                {
                                    await parkEntrances[_outEntranceDto.Id]?.SetInfo(outRcode);

                                    await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                                    await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                }
                                else
                                {

                                    await this.ShowMessageAsync("提示", "出场失败!");
                                }
                                return;
                            }

                            await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.CalculationFee); //播报语音
                            var tollWindow = new ChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, (parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), _repositoryFareRule, _repositoryRangeTime,null);
                            tollWindow.Init();
                            var isFree = tollWindow.ShowDialog();
                            if (isFree.HasValue && isFree.Value)
                            {


                                await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                                await parkEntrances[_outEntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);


                                await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音
                            }
                        }
                    }
                    else
                    {  //临时车

                        DateTime outTime = DateTime.Now;

                        FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);

                        var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                        fareRule.TimeRangeList = rangTimes;

                        var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;

                        var carOutModel = new CarOutModel()
                        {
                            CarInRecord = isCarIn.CarInRecord,
                            InOutType = Enum.InOutTypeEnum.Artificial,
                            OutTime = outTime,
                            Receivable = receivable,
                            ParkId = _outEntranceDto.ParkLevel.Park.Id
                            
                        };

                        if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                        {
                            var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                            if (outRcode != null)
                            {
                                await parkEntrances[_outEntranceDto.Id]?.SetInfo(outRcode);

                                await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                                await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音
                            }
                            else
                            {

                                await this.ShowMessageAsync("提示", "出场失败!");
                            }
                            return;
                        }

                        await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.CalculationFee); //播报语音
                        var tollWindow = new ChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, (parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), _repositoryFareRule, _repositoryRangeTime,null);
                        tollWindow.Init();
                        var isFree = tollWindow.ShowDialog();
                        if (isFree.HasValue && isFree.Value)
                        {


                            await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                            await parkEntrances[_outEntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);


                            await _ledManager.SpeakAndShowText((parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), carOutModel, OutEnum.SuccessfulPayment); //播报语音


                        }
                    }

                }
                else
                {
                    Logger.Info(carNumber + " 无场内记录");
                    //无在场记录如果为月租车直接放行  否则弹出收费框
                    var user = _carNumberPermission.GetUser(_outEntranceDto.ParkLevel.Park.Id, carNumber);
                    if (user != null)
                    {
                        var model = new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now, ParkId = _outEntranceDto.ParkLevel.Park.Id };
                        var outRcode = _vehicleFlow.CarOut(carNumber, user, model);
                        if (outRcode != null)
                        {

                            await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                            await parkEntrances[_outEntranceDto.Id]?.SetInfo(outRcode);


                            await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), model, OutEnum.SuccessfulPayment); //播报语音
                        }
                        else
                        {

                            await this.ShowMessageAsync("提示", "出场失败!");
                        }
                        return;
                    }
                    else
                    {
                        FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);
                        var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                        fareRule.TimeRangeList = rangTimes;

                        ///弹出收费框
                        var tollWindow = new ChargerWindow(_ledManager, carNumber, fareRule, InOutTypeEnum.Artificial ,_repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, (parkEntrances[_inEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), _repositoryFareRule, _repositoryRangeTime,null);
                        tollWindow.Init();

                        var isFree = tollWindow.ShowDialog();
                        if (isFree.HasValue && isFree.Value)
                        {


                            await parkEntrances[_outEntranceDto.Id]?.OpenRod();
                            await parkEntrances[_outEntranceDto.Id]?.SetInfo( tollWindow.CarOutRecord);


                            await _ledManager.SpeakAndShowText((parkEntrances[_outEntranceDto.Id] as ParkEntranceInfo)?.GetDeviceInfo(), tollWindow.CarOutModel, OutEnum.SuccessfulPayment); //播报语音


                        }
                        return;
                    }
                }
                #endregion

                unitOfWork.Complete();
            }
        }


        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;
        const int SC_RESTORE = 0xF120;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        // prevent user from moving the window
                        handled = true;
                    }
                    else if (command == SC_RESTORE && WindowState == WindowState.Maximized)
                    {
                        // prevent user from restoring the window while it is maximized
                        // (but allow restoring when it is minimized)
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }

        private void AbpWindow_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        private  void AbpWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            var result = MessageBox.Show("确定要退出吗？", "提示", MessageBoxButton.OKCancel);
            if(result== MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void AbpWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TiggleFlyout(0);
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            TiggleFlyout(1);

        }

       

        

        public void Message(string titi, string message)
        {
            SynchronizationContext.Post(async (x) =>
            {
                await this.ShowMessageAsync(titi, message);
            }, null);
        }
        

        private void AbpWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //_parkMainControl.StartThreads();
        }
    }
}
