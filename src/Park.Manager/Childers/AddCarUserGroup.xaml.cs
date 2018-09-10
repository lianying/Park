using Park.Childers.ViewModels;
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
using MahApps.Metro.Controls;
using Park.CarUserGroups;

namespace Park.Childers
{
    /// <summary>
    /// AddCarUserGroup.xaml 的交互逻辑
    /// </summary>
    public partial class AddCarUserGroup 
    {
        private AddCarUserGroupViewModel addCarUserGroupViewModel;
        private readonly ICarUserGroupAppService _carUserGroupAppService;
        public AddCarUserGroup(ICarUserGroupAppService  carUserGroupAppService)
        {
            addCarUserGroupViewModel = new AddCarUserGroupViewModel();
            addCarUserGroupViewModel.CarUserGroup = new CarUserGroups.Dtos.CarUserGroupEditDto();
            DataContext = addCarUserGroupViewModel;
            _carUserGroupAppService = carUserGroupAppService;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (addCarUserGroupViewModel.CarUserGroup.ParkArea == null)
            {
                await window.ShowMessageAsync("提示", "请选择区域");
                return;
            }
            await _carUserGroupAppService.CreateOrUpdateCarUserGroup(new CarUserGroups.Dtos.CreateOrUpdateCarUserGroupInput() { CarUserGroup = addCarUserGroupViewModel.CarUserGroup });
            DialogResult = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
