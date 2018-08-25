using Park.Parks.Park;
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
using System.Windows.Shapes;

namespace Park.Childers
{
    /// <summary>
    /// AddPark.xaml 的交互逻辑
    /// </summary>
    public partial class AddPark
    {
        private readonly IParkAppService _parkAppService;
        public AddPark(IParkAppService parkAppService)
        {
            InitializeComponent();
            _parkAppService = parkAppService;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if(!int.TryParse(Txt_CarportCount.Text.Trim(),out count))
            {
                MessageBox.Show("请输入正确的车位数");
                return;
            }
            await _parkAppService.Create(new Parks.Park.Dto.CreateParkDto() { Address = Txt_Address.Text, AreaCode = "330103", Latitude = 30.3073200M, Longitude = 120.1758920M, IsSync = true, Name = Txt_Name.Text.Trim(), CarportCount = count, Operator = Cmb_Operator.SelectedValue == null ? string.Empty : Cmb_Operator.SelectedValue.ToString(), PropertyParty = Cmb_PropertyParty.SelectedValue == null ? string.Empty : Cmb_PropertyParty.SelectedValue.ToString() });

            this.DialogResult = true;
            this.Close();
        }
    }
}
