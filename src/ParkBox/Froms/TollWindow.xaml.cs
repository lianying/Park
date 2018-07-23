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

namespace Park.Froms
{
    /// <summary>
    /// TollWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TollWindow
    {
        private readonly IRepository<CarTypes, long> _repositoryCarTypes;

        private readonly IRepository<CarPort, long> _repositoryCarPort;

        private readonly LedManager _ledManager;

        private readonly IParkBoxOptions _parkBoxOptions;

        private readonly IVehicleFlow _vehicleFlow;

        public decimal LocalMoneyOrTime { get; private set; }


        private decimal pay;
        public decimal Pay
        {
            get
            {
                if (pay != 0)
                    return pay;
                var temp = CarOutModel.CarInRecord?.AdvancePayment;

                pay = Receivable - disCountMoney - (temp.HasValue ? temp.Value : 0) - LocalMoneyOrTime;
                return pay >= 0 ? pay : 0;
            }
            set { pay = value; }
        }

        public decimal Receivable { get;private set; }


        public CarOutModel CarOutModel { get;private set; }


        public OutOfferTypeEnum OutOfferType { get; private set; }


        public string CarNumber { get;private set; }


        public FareRule FareRule { get;private set; }


        private decimal disCountMoney;

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
                        disCountMoney = Receivable;
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

        
        


        



        public TollWindow(LedManager ledManager,FareRule fareRule,
            IRepository<CarTypes,long> repositoryCarTypes,
            IParkBoxOptions parkBoxOptions,
            IRepository<CarPort,long> repositoryCarPort,
            IVehicleFlow vehicleFlow)
        {
            _ledManager = ledManager;
            OutOfferType = OutOfferTypeEnum.NormalPass;
            FareRule = fareRule;
            _repositoryCarTypes = repositoryCarTypes;
            _parkBoxOptions = parkBoxOptions;
            _repositoryCarPort = repositoryCarPort;
            _vehicleFlow = vehicleFlow;
            InitializeComponent();
        }


        public TollWindow (LedManager ledManager, CarOutModel carOutModel, FareRule fareRule,decimal receivable,
            IRepository<CarTypes,long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
            IRepository<CarPort, long> repositoryCarPort,
            IVehicleFlow vehicleFlow) :this(ledManager, fareRule, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow)
        {

            CarOutModel = carOutModel;
            CarNumber = carOutModel.CarInRecord?.CarNumber;
            Receivable = receivable;

        }

        public void Init()
        {
            txt_CarNumber.Text = CarNumber;
            if (CarOutModel != null)
            {
                if (CarOutModel.CarInRecord.IsMonthTempIn)
                {
                    txt_CarType.Text = "月租车（车位满以临时车入场）";
                }
                else
                {
                    if (FareRule.CarTypeId == _parkBoxOptions.TempCarTypeId)
                    {
                        txt_CarType.Text = "临时车";
                    }
                    else {
                        var carPort = _repositoryCarTypes.GetAll().FirstOrDefault(x => x.Id == CarOutModel.CarInRecord.CarPort.CarPortTypeId);
                        txt_CarType.Text = carPort?.CustomName;
                    }
                }
                txt_CarInTime.Text = CarOutModel.CarInRecord.InTime.ToTimeString();
                txt_CarOutTime.Text = CarOutModel.OutTime.ToTimeString();
                txt_FeeTime.Text = FareRule.FreeTime.ToString("#0.00");
                txt_Receivable.Text = Receivable.ToString("#0.00");
                txt_DiscountedPrice.Text = DisCountMoney.ToString("#0.00");
                txt_AdvancePayment.Text = CarOutModel.CarInRecord?.AdvancePayment.ToString("#0.00");
                var pay = Receivable - disCountMoney - CarOutModel.CarInRecord?.AdvancePayment - LocalMoneyOrTime;


            }
        }

        
        


        public TollWindow(LedManager ledManager, string CarNumber,FareRule fareRule, 
            IRepository<CarTypes, long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
             IRepository<CarPort, long> repositoryCarPort,
             IVehicleFlow vehicleFlow) : this(ledManager,fareRule, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow) {
            
        }



        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
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
                else {
                    if (Tsh_Money.IsChecked.HasValue && !Tsh_Money.IsChecked.Value) {
                        Spl_MoneyOrTime.Visibility = Visibility.Collapsed;
                    }
                }
            }
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
            var obj = sender as RadioButton;
            if (obj.Content.ToString() == "其他原因")
            {
                Spl_OtherText.Visibility = Visibility.Visible;
                txt_Other.Text = "";
            }
            else {
                Spl_OtherText.Visibility = Visibility.Collapsed;
                txt_Other.Text = obj.Content.ToString();
            }
        }

        

    }
}
