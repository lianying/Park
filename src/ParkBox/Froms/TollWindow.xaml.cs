using Park.Entitys.Box;
using Park.Enum;
using System.Windows;
using Park.Parks.ParkBox.Core;
using System.Windows.Controls;

namespace Park.Froms
{
    /// <summary>
    /// TollWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TollWindow
    {
        private readonly LedManager _ledManager;

        public decimal LocalMoneyOrTime { get; private set; }

        public decimal Pay { get;private set; }

        public decimal Receivable { get;private set; }


        public CarOutModel CarOutModel { get;private set; }


        public OutOfferTypeEnum OutOfferType { get; private set; }


        public string CarNumber { get; set; }


        public 



        public TollWindow(LedManager ledManager)
        {
            _ledManager = ledManager;
            OutOfferType = OutOfferTypeEnum.NormalPass;
            InitializeComponent();
        }


        public TollWindow (LedManager ledManager, CarOutModel carOutModel, decimal receivable) :this(ledManager)
        {
            
            CarInRecord = carOutModel;
            Receivable = receivable;
        }


        public TollWindow(LedManager ledManager, string CarNumber) : this(ledManager) {
            
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
