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
using Abp.Dependency;
using Park.CarPorts;
using Park.CarPorts.Dtos;
using Park.Childers.ViewModels;
using Park.ViewModel;

namespace Park.Childers
{
    /// <summary>
    /// AddCarport.xaml 的交互逻辑
    /// </summary>
    public partial class AddCarports : ITransientDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ICarPortAppService _carPortAppService;
        private AddPortsViewModel _addPortsViewModel;
        public AddCarports(MainWindowViewModel mainWindowViewModel, ICarPortAppService carPortAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _addPortsViewModel = new AddPortsViewModel();
            this.DataContext = _addPortsViewModel;
            _carPortAppService = carPortAppService;
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _addPortsViewModel.CarPortEditDto.CarPortTypeId = (long)Cmb_Area.SelectedValue;
            _addPortsViewModel.CarPortEditDto.AreaId = (long)Cmb_CarType.SelectedValue;
            await _carPortAppService.CreateOrUpdateCarPort(new CreateOrUpdateCarPortInput() { CarPort = _addPortsViewModel.CarPortEditDto });

            this.DialogResult = true;

            this.Close();
        }
    }
}
