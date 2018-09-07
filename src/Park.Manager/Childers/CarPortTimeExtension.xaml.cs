using Park.CarPorts;
using Park.CarPorts.Dtos;
using Park.Childers.ViewModels;
using Park.RechargeRecords;
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

namespace Park.Childers
{
    /// <summary>
    /// CarPortTimeExtension.xaml 的交互逻辑
    /// </summary>
    public partial class CarPortTimeExtension 
    {
        private MainWindowViewModel _mainWindowViewModel;
        private CarPortTimeExTenstionViewModel _carPortTimeExTenstionViewModel;
        private readonly ICarPortAppService _carPortAppService;
        private UserManagerViewModel _userManagerViewModel;
        private readonly IRechargeRecordAppService _rechargeRecordAppService;


        public CarPortTimeExtension(ICarPortAppService carPortAppService, MainWindowViewModel mainWindowViewModel, UserManagerViewModel userManagerViewModel,CarPortListDto carPortListDto)
        {
            InitializeComponent();
            _carPortAppService = carPortAppService;
            _mainWindowViewModel = mainWindowViewModel;
            _userManagerViewModel = userManagerViewModel;
            _carPortTimeExTenstionViewModel = new CarPortTimeExTenstionViewModel();
            _carPortTimeExTenstionViewModel.CarPortListDto = carPortListDto;
            DataContext = _carPortTimeExTenstionViewModel;
            _rechargeRecordAppService = IocManager.Resolve<IRechargeRecordAppService>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
