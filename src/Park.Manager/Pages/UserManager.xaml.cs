using Park.CarUserses;
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
using Abp.Dependency;
using Park.ParkAreases;

namespace Park.Pages
{
    /// <summary>
    /// UserManager.xaml 的交互逻辑
    /// </summary>
    public partial class UserManager : Page,ISingletonDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ICarUsersAppService _carUsersAppService;
        private readonly IParkAreasAppService _parkAreasAppService;
        public UserManager(MainWindowViewModel mainWindowViewModel,
            ICarUsersAppService carUsersAppService,
            IParkAreasAppService parkAreasAppService
            )
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _carUsersAppService = carUsersAppService;
            _parkAreasAppService = parkAreasAppService;
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
