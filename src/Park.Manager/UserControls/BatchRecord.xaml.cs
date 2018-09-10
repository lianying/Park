using Park.CarPorts;
using Park.CarPorts.Dtos;
using Park.Childers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.UserControls
{
    /// <summary>
    /// CarportInfo.xaml 的交互逻辑
    /// </summary>
    public partial class BatchRecord : UserControl
    {

        private MainWindowViewModel _mainWindowViewModel;
        private CarPortTimeExTenstionViewModel _carPortTimeExTenstionViewModel;
        private readonly ICarPortAppService _carPortAppService;
        private UserManagerViewModel _userManagerViewModel;
        private CarPortListDto _carPortListDto;
        public RoutedEventHandler RoutedEventHandler;
        public BatchRecord(ICarPortAppService carPortAppService, MainWindowViewModel mainWindowViewModel, UserManagerViewModel userManagerViewModel, CarPortListDto carPortListDto)
        {
            InitializeComponent();
            _carPortListDto = carPortListDto;
            _carPortAppService = carPortAppService;
            _mainWindowViewModel = mainWindowViewModel;
            _userManagerViewModel = userManagerViewModel;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CarPortTimeExtension carPortTimeExtension = new CarPortTimeExtension(_carPortAppService, _mainWindowViewModel, _userManagerViewModel, _carPortListDto);
            var result = carPortTimeExtension.ShowDialog();
            if (result.HasValue && result.Value)
            {
                RoutedEventHandler?.Invoke(sender, e);
            }
            
        }
    }
}
