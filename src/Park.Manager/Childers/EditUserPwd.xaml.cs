
using MahApps.Metro.Controls;
using Park.Authorization;
using Park.Authorization.Users;
using Park.Childers.ViewModels;
using Park.ViewModel;
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
using MahApps.Metro.Controls.Dialogs;

namespace Park.Childers
{

   
    /// <summary>
    /// EditUserPwd.xaml 的交互逻辑
    /// </summary>
    public partial class EditUserPwd 
    {
        private EditPwdViewModel editPwdViewModel;

        private readonly LogInManager _logInManager;
        private readonly UserManager _userManager;
        private readonly MainWindowViewModel _mainWindowViewModel;
        public EditUserPwd(LogInManager logInManager, UserManager userManager,MainWindowViewModel mainWindowViewModel)
        {
            editPwdViewModel = new EditPwdViewModel();
            _logInManager = logInManager;
            _userManager = userManager;
            _mainWindowViewModel = mainWindowViewModel;
            InitializeComponent();
            DataContext = editPwdViewModel;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MetroWindow;
            if (editPwdViewModel.SureNewPwd != editPwdViewModel.NewPwd) {

                await main.ShowMessageAsync("提示", "两次输入密码不一致"); ;
                return;
            }
            var result = await _userManager.ChangePasswordAsync(AbpSession.UserId.Value, editPwdViewModel.OldPwd, editPwdViewModel.NewPwd);
            if (!result.Succeeded)
            {
                await main.ShowMessageAsync("提示", "请输入正确的密码"); ;
            }
            else {
                await main.ShowMessageAsync("提示", "密码修改成功");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
