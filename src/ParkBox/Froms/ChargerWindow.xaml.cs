using Park.Entitys.Box;
using Park.Enum;
using System.Windows;
using Park.Parks.ParkBox.Core;
using System.Windows.Controls;
using Park.Parks.ParkBox;
using Park.Entitys.FareRules;
using System.Linq;
using Abp.Domain.Repositories;
using Park.Entitys.CarTypes;
using Park.ParkBox;
using Park.Entitys.CarUsers;
using Park.Expansions;
using System;
using Park.Parks.ParkBox.Interfaces;
using Park.ViewModel;
using Park.Parks.Devices.Interfaces;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Threading;

namespace Park.Froms
{
    /// <summary>
    /// TollWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChargerWindow : ICharger
    {
        private readonly IRepository<CarTypes, long> _repositoryCarTypes;

        private readonly IRepository<CarPort, long> _repositoryCarPort;
        private readonly IRepository<FareRule> _repositoryFareRule;
        private readonly IRepository<RangeTime> _repositoryRangeTime;

        private readonly LedManager _ledManager;

        private readonly IParkBoxOptions _parkBoxOptions;

        private readonly IVehicleFlow _vehicleFlow;

        private readonly ChargerViewModel chargerViewModel;

        private PayTypeEnum payType = PayTypeEnum.Money;
        

        private DelayAction delayAction;
        


        public CarOutModel CarOutModel { get;private set; }


        public OutOfferTypeEnum OutOfferType { get; private set; }

        


        public FareRule FareRule { get;private set; }


        public CarOutRecord CarOutRecord { get; private set; }


        private decimal disCountMoney;
        
        public IDeviceable Deviceable { get; private set; }

        private ManualResetEvent _manualResetEvent;


        private bool? isClose;
        public bool? IsClose
        {
            get { return isClose; }
            set
            {
                this.isClose = value;
                _manualResetEvent?.Set();
            }
        }
        /// <summary>
        /// 平台下发优惠券
        /// </summary>
        public decimal DisCountMoney
        {
            get
            {
                if (disCountMoney != 0)
                {
                    return disCountMoney;
                }
                if (CarOutModel.CarDiscount != null)
                {
                    if (CarOutModel.CarDiscount.DiscountType == CarDiscountEnum.Money)
                    {
                        if (!decimal.TryParse(CarOutModel.CarDiscount.DiscountValue, out disCountMoney))
                        {
                            disCountMoney = 0;
                        }
                    }
                    else if (CarOutModel.CarDiscount.DiscountType == CarDiscountEnum.Free)
                    {
                        disCountMoney = chargerViewModel.Receivable;
                    }
                    else {
                        double time = 0;
                        double.TryParse(CarOutModel.CarDiscount.DiscountValue, out time);
                        disCountMoney = FareRule.CalculateFees(DateTime.Now, DateTime.Now.AddMinutes(time), 0, true);
                    }

                    return disCountMoney;
                }
                else {
                    return 0;
                }
            }
        }

        
        


        



        private ChargerWindow(LedManager ledManager,FareRule fareRule,
            IRepository<CarTypes,long> repositoryCarTypes,
            IParkBoxOptions parkBoxOptions,
            IRepository<CarPort,long> repositoryCarPort,
            IVehicleFlow vehicleFlow,
            IDeviceable device,
            IRepository<FareRule> repositoryFareRule,
            IRepository<RangeTime> repositoryRangeTime,
            ManualResetEvent  manualResetEvent)
        {

            InitializeComponent();
            _ledManager = ledManager;
            OutOfferType = OutOfferTypeEnum.NormalPass;
            FareRule = fareRule;
            _repositoryCarTypes = repositoryCarTypes;
            _parkBoxOptions = parkBoxOptions;
            _repositoryCarPort = repositoryCarPort;
            _vehicleFlow = vehicleFlow;
            _repositoryFareRule = repositoryFareRule;
            _repositoryRangeTime = repositoryRangeTime;
            delayAction = new DelayAction();
            chargerViewModel = new ChargerViewModel();
            this.DataContext = chargerViewModel;
            chargerViewModel.Remark = "正常缴费";
            _manualResetEvent = manualResetEvent;


        }


        public ChargerWindow(LedManager ledManager, CarOutModel carOutModel, FareRule fareRule,decimal receivable,
            IRepository<CarTypes,long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
            IRepository<CarPort, long> repositoryCarPort,
            IVehicleFlow vehicleFlow,
            IDeviceable deviceable, 
            IRepository<FareRule> repositoryFareRule,
            IRepository<RangeTime> repositoryRangeTime,
             ManualResetEvent manualResetEvent) :this(ledManager, fareRule, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow, deviceable, repositoryFareRule, repositoryRangeTime,manualResetEvent)
        {

            CarOutModel = carOutModel;
            SetUIModel(carOutModel, receivable);
        }

        private void SetUIModel(CarOutModel carOutModel,decimal receivable)
        {
            chargerViewModel.CarNumber = carOutModel.CarInRecord?.CarNumber;
            chargerViewModel.Receivable = receivable;
            chargerViewModel.LocalMoneyOrTime = 0;
            chargerViewModel.InTime = carOutModel.CarInRecord?.InTime ?? DateTime.Now;
            chargerViewModel.OutTime = carOutModel.OutTime;
            chargerViewModel.Pay = receivable - DisCountMoney - (carOutModel.CarInRecord?.AdvancePayment ?? 0);
            chargerViewModel.CarNumber = CarOutModel.CarInRecord?.CarNumber;
            chargerViewModel.AdvancePayment = carOutModel.CarInRecord?.AdvancePayment;
            chargerViewModel.DiscountedPrice = DisCountMoney;
        }

        private void SyncDoAction(Action action)
        {
            SynchronizationContext.Post((x) =>
            {
                action();
            }, null);
        }

        public void Init(bool isRestFare = false)
        {

            if (CarOutModel != null)
            {
                CarOutModel.CarDiscount = _vehicleFlow.GetCarDiscount(CarOutModel.ParkId, CarOutModel.CarInRecord?.CarNumber);
                if (CarOutModel.CarInRecord.IsMonthTempIn)
                {
                    SyncDoAction(() => txt_CarType.Text = "月租车（车位满以临时车入场）");
                }
                else
                {
                    var id = CarOutModel.CarInRecord.CarPort?.CarPortTypeId;
                    var carPort = _repositoryCarTypes.GetAll().FirstOrDefault(x => x.Id == (id ?? 0));
                    if (isRestFare)
                    {

                        var outTime = DateTime.Now;
                        if (carPort == null || carPort.Category == CarType.Month)
                        {
                            FareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == _parkBoxOptions.TempCarTypeId);
                            FareRule.TimeRangeList = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == FareRule.Id).ToList();
                            CarOutModel.Receivable = FareRule.CalculateFees(CarOutModel.CarInRecord.InTime, outTime, 0);
                        }
                        else
                        {
                            FareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == carPort.Id);
                            FareRule.TimeRangeList = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == FareRule.Id).ToList();
                        }
                        if (CarOutModel.CarInRecord.IsMonthTempIn && CarOutModel.CarInRecord.TempConvertMonthTime.HasValue)
                        {
                            outTime = CarOutModel.CarInRecord.TempConvertMonthTime.Value;

                            CarOutModel.Receivable = FareRule.CalculateFees(CarOutModel.CarInRecord.InTime, outTime, 0);
                        }
                    }
                    if (carPort == null || carPort.Id == _parkBoxOptions.TempCarTypeId)
                    {
                        SyncDoAction(()=> txt_CarType.Text = "临时车");
                    }
                    else
                    {
                        SyncDoAction(() => txt_CarType.Text = carPort?.CustomName);
                    }
                }
            }
        }





        public ChargerWindow(LedManager ledManager, string carNumber, FareRule fareRule,InOutTypeEnum inOutType,
            IRepository<CarTypes, long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
             IRepository<CarPort, long> repositoryCarPort,
             IVehicleFlow vehicleFlow,
             IDeviceable deviceable,
             IRepository<FareRule> repositoryFareRule,
            IRepository<RangeTime> repositoryRangeTime,
             ManualResetEvent manualResetEvent) : this(ledManager, fareRule, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow, deviceable, repositoryFareRule,repositoryRangeTime,manualResetEvent)
        {
            var inTime = DateTime.Now;
            var list = _vehicleFlow.LevenshteinDistance(parkBoxOptions.ParkId, carNumber);
            chargerViewModel.InTime = chargerViewModel.OutTime = inTime;
            chargerViewModel.CarNumber = carNumber;
            CarOutModel = new CarOutModel() { CarInRecord = new CarInRecord() { CarNumber = carNumber, InTime = inTime, ParkId = deviceable.EntranceDto.ParkLevel.Park.Id }, OutTime = inTime, ParkId = deviceable.EntranceDto.ParkLevel.Park.Id };
            AddMenu(list);
            txt_CarType.Text = "无在场记录，请匹配场内车辆";
        }


        private void AddMenu(List<CarInRecord> carInRecords) {
            wpl_Menu.Items.Clear();


            MenuItem menuItem;
            foreach (var item in carInRecords)
            {
                menuItem = new MenuItem();
                menuItem.Tag = item;
                menuItem.Header = item.CarNumber;
                menuItem.Click += MenuItem_Click;
                wpl_Menu.Items.Add(menuItem);
            }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                if (menuItem.Header.ToString() != "其他")
                {
                    SetCarInModel(menuItem.Tag as CarInRecord);
                }
                else  //其他
                {

                }
            }
        }

        private void SetCarInModel(CarInRecord carInRecord)
        {
            CarOutModel = new CarOutModel();
            CarOutModel.CarInRecord = carInRecord;
            CarOutModel.OutTime = DateTime.Now;
            Init(true);
            SetUIModel(CarOutModel, CarOutModel.Receivable);
        }

        private void Tsh_Money_IsCheckedChanged(object sender, System.EventArgs e)
        {
            if (Tsh_Money.IsChecked.HasValue)
            {
                if (Tsh_Money.IsChecked.Value)
                {
                    Spl_MoneyOrTime.Visibility = Visibility.Visible;
                    Tsh_Time.IsChecked = false;
                    lbl_MoneyOrTime.Content = "金额:";
                    txt_MoneyOrTime.Text = "0";
                }
                else
                {
                    if (Tsh_Time.IsChecked.HasValue && !Tsh_Time.IsChecked.Value)
                    {
                        Spl_MoneyOrTime.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void Chk_NormalPassage_Checked(object sender, RoutedEventArgs e)
        {
            if (spl_OtherText != null)
            {
                var obj = sender as RadioButton;
                if (obj.Content.ToString() == "其他原因")
                {
                    spl_OtherText.Visibility = Visibility.Visible;
                    txt_Other.Text = "";
                }
                else
                {
                    spl_OtherText.Visibility = Visibility.Collapsed;
                    txt_Other.Text = obj.Content.ToString();
                }
            }
        }

        private void Tsh_Time_IsCheckedChanged(object sender, EventArgs e)
        {
            if (Tsh_Time.IsChecked.HasValue)
            {
                if (Tsh_Time.IsChecked.Value)
                {
                    Spl_MoneyOrTime.Visibility = Visibility.Visible;
                    Tsh_Money.IsChecked = false;
                    lbl_MoneyOrTime.Content = "时长:";
                    txt_MoneyOrTime.Text = "0";
                }
                else
                {
                    if (Tsh_Money.IsChecked.HasValue && !Tsh_Money.IsChecked.Value)
                    {
                        Spl_MoneyOrTime.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public new  bool? ShowDialog()
        {
            //bool? flg = false;
            //SynchronizationContext.Post((x) => {
            //    flg = base.ShowDialog();
            //}, null);
            //return flg;
            return base.ShowDialog();

        }

        public new void Show()
        {
            SynchronizationContext.Post((x) => {
                base.Show();
            }, null);
            
        }

        private  void txt_MoneyOrTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            delayAction.Debounce(500, SynchronizationContext,async () =>
            {

                if (!chargerViewModel.LocalMoneyOrTime.HasValue)
                    return;
                if (chargerViewModel.LocalMoneyOrTime.HasValue && chargerViewModel.LocalMoneyOrTime == 0)
                    return;
                //计算金额
                if (Tsh_Time.IsChecked.HasValue && Tsh_Time.IsChecked.Value)
                {

                    var money = FareRule?.CalculateFees(DateTime.Now, DateTime.Now.AddMinutes((double)chargerViewModel.LocalMoneyOrTime.Value), 0, true) ?? 0;

                    chargerViewModel.Pay -= money;

                }
                else if (Tsh_Money.IsChecked.HasValue && Tsh_Money.IsChecked.Value) {

                    chargerViewModel.Pay -= chargerViewModel.LocalMoneyOrTime.Value;
                }

                //更新显示屏
                await _ledManager.SpeakAndShowText(Deviceable, CarOutModel, OutEnum.CalculationFee);

                //调用平台接口



            });
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            
            CarOut(false);
            this.Close();
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            CarOut(true);
            this.Close();
        }



        private void CarOut(bool isErrorOut)
        {
            SetCarOutModel(isErrorOut);
            CarOutRecord = _vehicleFlow.CarOut(CarOutModel.CarInRecord, CarOutModel);
            if (CarOutRecord == null)
            {
                this.ShowMessageAsync("提示", "出场失败");
                return;
            }
            if (this._manualResetEvent == null)
            {
                this.DialogResult = true;
            }
            else
            {
                IsClose = isErrorOut;
            }
        }

        private void SetCarOutModel()
        {
            CarOutModel.Receivable = chargerViewModel.Receivable;
            CarOutModel.Pay = chargerViewModel.Pay;
            CarOutModel.Remark = chargerViewModel.Remark;
            CarOutModel.PayType = payType;
            if (chargerViewModel.IsTimeChecked)
            {
                CarOutModel.OfferTime = TimeSpan.FromMinutes((double)(chargerViewModel.LocalMoneyOrTime ?? 0));
            }
            else if (chargerViewModel.IsMoneyChecked)
            {
                CarOutModel.OfferMoney = chargerViewModel.LocalMoneyOrTime ?? 0;
            }
            
        }
        private void SetCarOutModel(bool isErrorOut)
        {
            CarOutModel.IsErrorOut = isErrorOut;
            SetCarOutModel();
        }

        private void WrapPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            wpl_Menu.IsOpen = true;
        }
    }
}
