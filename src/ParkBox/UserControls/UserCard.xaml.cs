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
using Abp.Dependency;
using System.Windows.Threading;
using Park.Expansions;

namespace Park.UserControls
{
    /// <summary>
    /// UserCard.xaml 的交互逻辑
    /// </summary>
    public partial class UserCard : UserControl,ITransientDependency
    {
        private readonly IParkBoxOptions parkBoxOptions;

        private SynchronizationContext SynchronizationContext;

        private DateTime loginTime;

        private Timer onlineTime;
        public UserCard(IParkBoxOptions parkBoxOptions)
        {
            InitializeComponent();
            SynchronizationContext = DispatcherSynchronizationContext.Current;
            this.parkBoxOptions = parkBoxOptions;
            loginTime = DateTime.Now;
            onlineTime = new Timer(x=> {
                SynchronizationContext.Post(z =>
                {
                    var time = DateTime.Now - loginTime;
                    this.txt_OnlineTime.Text = time.Hours + "小时" + time.Minutes + "分";
                }, null);
            },null,1000,60000);
            InitUserImge();
            SetUserInfo();
            this.txt_FeeMoney.Text = "0.00 元";
        }

        private void InitUserImge()
        {
            if (parkBoxOptions.UserImg != null)
            {

                UserPhoto.SetImage(parkBoxOptions.UserImg);
            }
            else {

                UserPhoto.SetImage(parkBoxOptions.DefultUserImg);
            }
        }

        public void SetUserInfo()
        {
            SynchronizationContext.Post(x =>
            {
                this.txt_UserName.Text = parkBoxOptions.User.Name;
                this.txt_UserCode.Text = parkBoxOptions.User.Id.ToString();
            }, null);
        }
        /// <summary>
        /// 结算
        /// </summary>
        public void Settlement()
        {
            
        }

        /// <summary>
        /// 换班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ChangeShift_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_Settlement_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_LoginOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
